using Microsoft.AspNetCore.Mvc;
using InvestmentPredictor; // Twoja biblioteka

namespace InvestmentPredictor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Adres to będzie: api/investment
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentCalculator _calculator;

        // ASP.NET sam poda (wstrzyknie) tu kalkulator dzięki rejestracji w Program.cs
        public InvestmentController(IInvestmentCalculator calculator)
        {
            _calculator = calculator;
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] CalculatorParams p)
        {
            // Zwracamy obiekt anonimowy, który ASP.NET zamieni na JSON
            decimal rate = p.chosenIndex != MarketIndex.Custom
                ? _calculator.GetIndexAnnualReturn(p.chosenIndex)
                : (p.customAnnualReturn ?? 0);

            var finalParams = p with { annualReturn = rate };


            return Ok(new
            {
                UsedRate = rate,

                TotalValue = _calculator.CalculateTotalValue(finalParams),
                PureProfit = _calculator.GetPureReturnValue(finalParams),
                TaxValue = _calculator.CalculateTaxValue(finalParams),
                TotalNet = _calculator.TotalValueAfterTax(finalParams)
            });
        }
    }
}