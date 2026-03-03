namespace InvestmentPredictor
{
    public class InvestmentCalculator : IInvestmentCalculator
    {
        public decimal GetIndexAnnualReturn(MarketIndex index) => index switch
        {
            MarketIndex.SP500 => 10.4m,
            MarketIndex.Nasdaq100 => 16.2m,
            MarketIndex.WIG20 => 5.2m,
            MarketIndex.Gold => 8.5m,
            MarketIndex.MSCIWorld => 8.9m,
            MarketIndex.Russell2000=> 8.2m,
            MarketIndex.MSCIEmergingMarkets => 5.9m,
            MarketIndex.DowJones => 7.4m,
            _ => 0m


        };
       public decimal CalculatedValue(decimal monthlySubsidy, decimal initialAmount, decimal annualReturn, int period)
        {
            decimal calculatedValue = initialAmount;
            for (int year = 1; year <= period; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    calculatedValue += monthlySubsidy;
                    calculatedValue += calculatedValue * annualReturn / 100 / 12;
                }


            }
            return calculatedValue;
        }

      public  decimal getPureReturnValue(decimal monthlySubsidy, decimal initialAmount, decimal annualReturn, int period)
        {
            decimal calculatedValue = CalculatedValue(monthlySubsidy, initialAmount, annualReturn, period);

            decimal sumOfSubsidy = initialAmount + (monthlySubsidy * 12 * period);


            return calculatedValue-sumOfSubsidy;



        }

        public decimal CalculateTaxValue(decimal monthlySubsidy, decimal initialAmount, decimal annualReturn, int period)
        {
            const decimal taxRate= 0.19m;
            decimal pureProfit= getPureReturnValue(monthlySubsidy, initialAmount, annualReturn, period); ;
            return pureProfit - pureProfit * taxRate;

        }

        public decimal totalValueAfterTax(decimal monthlySubsidy, decimal initialAmount, decimal annualReturn, int period) {
            decimal pureProfitAfterTax = CalculateTaxValue(monthlySubsidy, initialAmount, annualReturn, period);
            decimal pureProfit= getPureReturnValue(monthlySubsidy,initialAmount, annualReturn, period);
            return (CalculatedValue(monthlySubsidy, initialAmount, annualReturn, period) - pureProfit) + pureProfitAfterTax;
        }
        


    }
}
