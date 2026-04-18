namespace InvestmentPredictor
{
    public class InvestmentCalculator : IInvestmentCalculator
    {
        public decimal GetIndexAnnualReturn(MarketIndex index) => index switch
        {
            // Avg annual return (aprox. last 20 years)
            MarketIndex.SP500 => 10.4m,
            MarketIndex.Nasdaq100 => 14.4m,
            MarketIndex.WIG20 => 4.8m,
            MarketIndex.Gold => 10.7m,
            MarketIndex.Silver => 10.0m,
            MarketIndex.MSCIWorld => 8.5m,
            MarketIndex.Russell2000=> 8.0m,
            MarketIndex.MSCIEmergingMarkets => 6.5m,
            MarketIndex.DowJones => 10.2m,
            MarketIndex.DAX => 6.5m,
            MarketIndex.Nikkei225 => 6.1m,
            _ => 0m


        };
       public decimal CalculateTotalValue(CalculatorParams p)
        {
            decimal calculatedValue = p.initialAmount;
            var monthlyRate = p.annualReturn / 100 / 12;

            for (int year = 1; year <= p.period; year++)
            {

                

                for (int month = 1; month <= 12; month++)
                {
                    calculatedValue += calculatedValue * monthlyRate;
                    calculatedValue += p.monthlySubsidy;
                }


            }
            return calculatedValue;
        }

      public  decimal GetPureReturnValue(CalculatorParams p)
        {
            decimal calculatedValue = CalculateTotalValue(p);

            decimal totalInvested = p.initialAmount + (p.monthlySubsidy * 12 * p.period);


            return calculatedValue-totalInvested;



        }

        public decimal CalculateTaxValue(CalculatorParams p)
        {
            const decimal taxRate= 0.19m;
            decimal pureProfit= GetPureReturnValue(p); ;
            return pureProfit * taxRate;

        }

        public decimal TotalValueAfterTax(CalculatorParams p) {
            return CalculateTotalValue(p) - CalculateTaxValue(p);
        }
         
        public List<decimal> GetYearlyProjection(CalculatorParams p) {
            decimal currentValue = p.initialAmount;
            List<decimal> currentValueList = new List<decimal>();
            currentValueList.Add(currentValue);
            var monthlyRate = p.annualReturn / 100 / 12;
            for (int year = 1; year <= p.period; year++)
            {


                for (int month = 1; month <= 12; month++)
                {
                    currentValue += currentValue * monthlyRate;
                    currentValue += p.monthlySubsidy;
                }
                
                
                
                currentValueList.Add(currentValue);

            }

            
            return currentValueList; }
        


    }
}
