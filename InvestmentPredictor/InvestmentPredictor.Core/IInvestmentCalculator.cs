using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPredictor
{

    public record CalculatorParams(decimal monthlySubsidy, decimal initialAmount, decimal annualReturn, int period);
    public interface IInvestmentCalculator
    {
        decimal GetIndexAnnualReturn(MarketIndex index);
        decimal CalculatedValue(CalculatorParams p);

        decimal getPureReturnValue(CalculatorParams p);

        decimal CalculateTaxValue(CalculatorParams p);
        decimal totalValueAfterTax(CalculatorParams p);
    }
}
