using System;
namespace InvestmentPredictor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserInterface.DisplayMenu("0.2"); //UI
            decimal annualReturn = UserInterface.GetAnnualReturn();
            Console.WriteLine($"Your annual return rate is {annualReturn}%.");
            decimal initialAmount = UserInterface.ReadDecimal("Enter initial investment amount: ");
            int period = UserInterface.ReadInt("How long do you want to invest? (years): ");
            decimal monthlySubsidy = UserInterface.ReadDecimal("How much do you want to subsidize monthly?: ");
            decimal calculatedValue = InvestmentCalculator.CalculatedValue(monthlySubsidy, initialAmount, annualReturn, period);


            Console.WriteLine($"Total value after {period} years: {calculatedValue:C} ");
        }

    }
}
