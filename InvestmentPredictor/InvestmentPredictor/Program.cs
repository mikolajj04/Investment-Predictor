using System;
namespace InvestmentPredictor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-Investment Predictor v0.1-");
            Console.WriteLine("Enter initial investment amount: ");
            
            
            if(!decimal.TryParse(Console.ReadLine(), out decimal initialAmount))
            {
                Console.Error.WriteLine("Enter proper value!");
                return;
            }
            Console.WriteLine("How long do you want to invest? (years): ");
           if(!int.TryParse(Console.ReadLine(), out int period))
            {
                Console.Error.WriteLine("Enter proper period of time!");
                return;
            }
            Console.WriteLine($"How much do you want to subsidize monthly?: ");
           if(!decimal.TryParse(Console.ReadLine(), out decimal monthlySubsidy))
            {
                Console.Error.WriteLine("Enter proper value!");
                return;
            }

            Console.WriteLine("Enter expected annual return rate (e.g. 10 for 10%): ");
            if(!decimal.TryParse(Console.ReadLine(), out decimal annualReturn))
            {
                Console.Error.WriteLine("Enter proper annual return rate!");
                return;
            }
            var calculator = new InvestmentCalculator();
            decimal calculatedValue = calculator.CalculatedValue(monthlySubsidy, initialAmount, annualReturn, period);
            
            
            Console.WriteLine($"Total value after {period} years:  {calculatedValue:C} ");
        }

    }
}
