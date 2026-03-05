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
            return Ok(new
            {
                TotalValue = _calculator.CalculatedValue(p),
                PureProfit = _calculator.GetPureReturnValue(p),
                TaxValue = _calculator.CalculateTaxValue(p),
                TotalNet = _calculator.TotalValueAfterTax(p)
            });
        }
    }
}