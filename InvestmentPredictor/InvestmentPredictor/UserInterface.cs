
namespace InvestmentPredictor
{
    public static class UserInterface
    {
        public static void DisplayMenu(string version)
        {

            Console.WriteLine($"----Investment Predictor v{version}----\n");
            Console.WriteLine("Select one of following indexes or choose custom option for custom ROI: ");
            Console.WriteLine("1. S&P 500 - 10.5%");
            Console.WriteLine("2. Nasdaq100 - 14,4%");
            Console.WriteLine("3. WIG20 - 6,2%");
            Console.WriteLine("4. Gold - 8,5%");
            Console.WriteLine("5. MSCI World - 8,9%");
            Console.WriteLine("6. Russell 2000 - 8,2%");
            Console.WriteLine("7. MSCIEmergingMarkets - 5,9%");
            Console.WriteLine("8. Dow Jones - 7,4%");
            Console.WriteLine("9. Custom");
            Console.WriteLine("---------------------------------");
        }

        public static decimal ReadDecimal(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out decimal result) && result > 0)
                {
                    return result;
                }
                else
                {
                    Console.Error.WriteLine("Invalid input! Please enter non-negative number.");
                }
            }


        }

        public static int ReadInt(string message)
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

        public static decimal GetAnnualReturn()
        {
            while (true)
            {
                decimal annualReturn;
                string input = Console.ReadLine();
                MarketIndex index = MarketIndex.Custom;
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

                        annualReturn = InvestmentCalculator.GetAnnualReturn(index);
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
