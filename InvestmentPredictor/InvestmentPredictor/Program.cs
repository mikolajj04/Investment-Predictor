using System;
namespace InvestmentPredictor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var calculator = new InvestmentCalculator();
            Console.WriteLine("-Investment Predictor v0.1-");
            Console.WriteLine("Select one of following indexes or choose custom option for custom ROI: ");
            Console.WriteLine("1. S&P 500 - 10.5%");
            Console.WriteLine("2. Nasdaq100 - 14,4%");
            Console.WriteLine("3. WIG20 - 6,2%");
            Console.WriteLine("4. Gold - 7,8%");
            Console.WriteLine("5. Custom");

            decimal annualReturn;
            ConsoleKeyInfo option = Console.ReadKey(true);
            char charOption = option.KeyChar;
            MarketIndex index = MarketIndex.Custom;
            if (char.IsDigit(charOption))
            {
                if ((MarketIndex)char.GetNumericValue(charOption)== MarketIndex.Custom)
                {
                    Console.WriteLine("Enter expected annual return rate (e.g. 10 for 10%): ");
                    if (!decimal.TryParse(Console.ReadLine(), out  annualReturn))
                    {
                        Console.Error.WriteLine("Enter proper annual return rate!");
                        return;
                    }
                }
                else { 
                index = (MarketIndex)(int)char.GetNumericValue(charOption);
                annualReturn = InvestmentCalculator.GetAnnualReturn(index);
                }
            }else{

                Console.Error.WriteLine("Invalid option! ");
                return;
            }
          


            Console.WriteLine("Enter initial investment amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal initialAmount))
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


          


            //Console.WriteLine("Enter expected annual return rate (e.g. 10 for 10%): ");
            //if(!decimal.TryParse(Console.ReadLine(), out decimal annualReturn))
            //{
            //    Console.Error.WriteLine("Enter proper annual return rate!");
            //    return;
            //}
            decimal calculatedValue = calculator.CalculatedValue(monthlySubsidy, initialAmount, annualReturn, period);
            
            
            Console.WriteLine($"Total value after {period} years:  {calculatedValue:C} ");
        }

    }
}
