namespace InvestmentPredictor
{
    public class InvestmentCalculator : IInvestmentCalculator
    {
        public decimal GetIndexAnnualReturn(MarketIndex index) => index switch
        {
            MarketIndex.SP500 => 10.4m,
            MarketIndex.Nasdaq100 => 14.4m,
            MarketIndex.WIG20 => 5.2m,
            MarketIndex.Gold => 8.5m,
            MarketIndex.MSCIWorld => 8.9m,
            MarketIndex.Russell2000=> 8.2m,
            MarketIndex.MSCIEmergingMarkets => 5.9m,
            MarketIndex.DowJones => 7.4m,
            _ => 0m


        };
       public decimal CalculatedValue(CalculatorParams p)
        {
            decimal calculatedValue = p.initialAmount;
            for (int year = 1; year <= p.period; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    calculatedValue += p.monthlySubsidy;
                    calculatedValue += calculatedValue * p.annualReturn / 100 / 12;
                }


            }
            return calculatedValue;
        }

      public  decimal GetPureReturnValue(CalculatorParams p)
        {
            decimal calculatedValue = CalculatedValue(p);

            decimal sumOfSubsidy = p.initialAmount + (p.monthlySubsidy * 12 * p.period);


            return calculatedValue-sumOfSubsidy;



        }

        public decimal CalculateTaxValue(CalculatorParams p)
        {
            const decimal taxRate= 0.19m;
            decimal pureProfit= GetPureReturnValue(p); ;
            return pureProfit * taxRate;

        }

        public decimal TotalValueAfterTax(CalculatorParams p) {
            return CalculatedValue(p) - CalculateTaxValue(p);
        }
        


    }
}
