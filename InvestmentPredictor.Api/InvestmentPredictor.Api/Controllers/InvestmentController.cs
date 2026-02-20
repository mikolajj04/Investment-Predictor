using Microsoft.AspNetCore.Mvc;

namespace InvestmentPredictor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentController : ControllerBase
    {
        private readonly InvestmentCalculator _calculator;

        // Tutaj wstrzykujemy nasz kalkulator (Dependency Injection)
        public InvestmentController(InvestmentCalculator calculator)
        {
            _calculator = calculator;
        }

        [HttpGet("calculate")]
        public IActionResult Calculate(decimal initialAmount, int period, decimal monthlySubsidy, decimal annualReturn)
        {
            // Wywołujemy Twoją logikę z klasy InvestmentCalculator
            var finalValue = _calculator.CalculatedValue(monthlySubsidy, initialAmount, annualReturn, period);

            // Zwracamy wynik jako czysty JSON
            return Ok(new
            {
                InitialAmount = initialAmount,
                FinalValue = finalValue,
                Years = period,
                Currency = "PLN"
            });
        }
    }
}