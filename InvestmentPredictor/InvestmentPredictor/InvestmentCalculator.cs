namespace InvestmentPredictor
{
    public class InvestmentCalculator
    {
        public static decimal GetAnnualReturn(MarketIndex index) => index switch
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
       public static decimal CalculatedValue(decimal monthlySubsidy, decimal initialAmount, decimal annualReturn, int period)
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

        public static void CalculateAnnualReturn() { 

        }

    }
}
