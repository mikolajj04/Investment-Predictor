using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InvestmentPredictor
{

    public record CalculatorParams(
        [Range(0, 1000000000)] decimal monthlySubsidy,
        [Range(0, 1000000000)] decimal initialAmount,
        [Range(0, 100)] decimal annualReturn,
        [Range(1,200)] int period);
    public interface IInvestmentCalculator
    {
        decimal GetIndexAnnualReturn(MarketIndex index);
        decimal CalculatedValue(CalculatorParams p);

        decimal GetPureReturnValue(CalculatorParams p);

        decimal CalculateTaxValue(CalculatorParams p);
        decimal TotalValueAfterTax(CalculatorParams p);
    }
}
