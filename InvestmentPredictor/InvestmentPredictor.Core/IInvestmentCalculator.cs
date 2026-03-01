using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPredictor
{
    public interface IInvestmentCalculator
    {
        decimal GetIndexAnnualReturn(MarketIndex index);
        decimal CalculatedValue(decimal monthlySubsidy, decimal initialAmount, decimal annualReturn, int period);

        decimal getPureReturnValue(decimal monthlySubsidy, decimal initialAmount, decimal annualReturn, int period);
    }
}
