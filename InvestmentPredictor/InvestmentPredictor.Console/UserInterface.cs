
namespace InvestmentPredictor
{
    public class UserInterface
    {
        private readonly IInvestmentCalculator _calculator;

        //DI
        public UserInterface(IInvestmentCalculator calculator)
        {
            _calculator = calculator;

        }

        public void DisplayMenu(string version)
        {
            
            Console.WriteLine($"----Investment Predictor v{version}----\n");
            Console.WriteLine("Select one of following indexes or choose custom option for custom ROI: ");
            Console.WriteLine($"1. S&P 500 - {_calculator.GetIndexAnnualReturn(MarketIndex.SP500)}%");
            Console.WriteLine($"2. Nasdaq100 - {_calculator.GetIndexAnnualReturn(MarketIndex.Nasdaq100)}%");
            Console.WriteLine($"3. WIG20 - {_calculator.GetIndexAnnualReturn(MarketIndex.WIG20)}%");
            Console.WriteLine($"4. Gold - {_calculator.GetIndexAnnualReturn(MarketIndex.Gold)}%");
            Console.WriteLine($"5. MSCI World - {_calculator.GetIndexAnnualReturn(MarketIndex.MSCIWorld)}%");
            Console.WriteLine($"6. Russell 2000 - {_calculator.GetIndexAnnualReturn(MarketIndex.Russell2000)}%");
            Console.WriteLine($"7. MSCIEmergingMarkets - {_calculator.GetIndexAnnualReturn(MarketIndex.MSCIEmergingMarkets)}%");
            Console.WriteLine($"8. Dow Jones - {_calculator.GetIndexAnnualReturn(MarketIndex.DowJones)}%");
            Console.WriteLine("9. Custom");
            Console.WriteLine("---------------------------------");
        }

        public decimal ReadDecimal(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out decimal result) && result >= 0)
                {
                    return result;
                }
                else
                {
                    Console.Error.WriteLine("Invalid input! Please enter non-negative number.");
                }
            }


        }

        public int ReadInt(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                string input = Console.ReadLine();
                if(int.TryParse(input, out int result)&& result>0)
                {
                    return result;
                }
                Console.Error.WriteLine("Invalid input! Please enter non-negative number.");
            }
        }

        public decimal GetAnnualReturn(out MarketIndex index)
        {
            while (true)
            {
                decimal annualReturn;
                string input = Console.ReadLine();
                index = MarketIndex.Custom;
                if (int.TryParse(input, out int choice))
                {
                    index = (MarketIndex)choice;
                    if (index == MarketIndex.Custom)
                    {
                        Console.WriteLine("Enter expected annual return rate (e.g. 10 for 10%): ");
                        if (!decimal.TryParse(Console.ReadLine(), out annualReturn) || annualReturn<0)
                        {
                            Console.Error.WriteLine("Enter proper annual return rate!");
                            continue;

                        }
                        return annualReturn;
                    }
                    else
                    {

                        annualReturn = _calculator.GetIndexAnnualReturn(index);
                        if (annualReturn == 0 && index != MarketIndex.Custom)
                        {
                            Console.Error.WriteLine("Choose option from the list!");
                            continue;
                        }


                    }
                    return annualReturn;
                }
                else
                {

                    Console.Error.WriteLine("Invalid option! ");
                    continue;
                }
                

            }
            
        }

    }
}
