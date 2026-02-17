namespace InvestmentPredictor
{
    public class InvestmentCalculator
    {
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
