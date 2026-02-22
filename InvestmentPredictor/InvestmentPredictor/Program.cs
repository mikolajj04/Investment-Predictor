using System;
namespace InvestmentPredictor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserInterface.DisplayMenu("0.2"); //UI
            decimal annualReturn = UserInterface.GetAnnualReturn();
            //decimal annualReturn;
            //string input = Console.ReadLine();
            //MarketIndex index = MarketIndex.Custom;
            //if (int.TryParse(input, out int choice))
            //{
            //    index = (MarketIndex)choice;
            //    if (index == MarketIndex.Custom)
            //    {
            //        Console.WriteLine("Enter expected annual return rate (e.g. 10 for 10%): ");
            //        if (!decimal.TryParse(Console.ReadLine(), out annualReturn))
            //        {
            //            Console.Error.WriteLine("Enter proper annual return rate!");
            //            return;
            //        }
            //    }
            //    else
            //    {

            //        annualReturn = InvestmentCalculator.GetAnnualReturn(index);
            //        if (annualReturn == 0 && index != MarketIndex.Custom)
            //        {
            //            Console.Error.WriteLine("Choose option from the list!");
            //            return;
            //        }


            //    }
            //}
            //else
            //{

            //    Console.Error.WriteLine("Invalid option! ");
            //    return;
            //}
            Console.WriteLine($"Your annual return rate is {annualReturn}%.");
            decimal initialAmount = UserInterface.ReadDecimal("Enter initial investment amount: ");
            int period = UserInterface.ReadInt("How long do you want to invest? (years): ");
            decimal monthlySubsidy = UserInterface.ReadDecimal("How much do you want to subsidize monthly?: ");
            decimal calculatedValue = InvestmentCalculator.CalculatedValue(monthlySubsidy, initialAmount, annualReturn, period);


            Console.WriteLine($"Total value after {period} years: {calculatedValue:C} ");
        }

    }
}
