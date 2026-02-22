namespace InvestmentPredictor
{
    public class InvestmentCalculator
    {
        public static decimal GetAnnualReturn(MarketIndex index) => index switch
        {
            MarketIndex.SP500 => 10.5m,
            MarketIndex.Nasdaq100 => 14.4m,
            MarketIndex.WIG20 => 6.2m,
            MarketIndex.Gold => 7.8m,
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

    }
}
