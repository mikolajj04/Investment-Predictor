using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InvestmentPredictor
{

    public record CalculatorParams(
         MarketIndex chosenIndex,
        [Range(0, 1000000000)] decimal monthlySubsidy,
        [Range(0, 1000000000)] decimal initialAmount,
        [Range(0, 100)] decimal? customAnnualReturn,
        [property: JsonIgnore] decimal annualReturn,
        [Range(1,200)] int period);
    public interface IInvestmentCalculator
    {
        decimal GetIndexAnnualReturn(MarketIndex index);
        decimal CalculateTotalValue(CalculatorParams p);

        decimal GetPureReturnValue(CalculatorParams p);

        decimal CalculateTaxValue(CalculatorParams p);
        decimal TotalValueAfterTax(CalculatorParams p);

        List<decimal> GetYearlyProjection(CalculatorParams p);
    }
}
