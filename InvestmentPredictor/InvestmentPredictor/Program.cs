using System;
namespace InvestmentPredictor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IInvestmentCalculator calculator = new InvestmentCalculator();
            UserInterface ui = new UserInterface(calculator);
            UserInterface.DisplayMenu("0.2"); //UI
            decimal annualReturn = ui.GetAnnualReturn();
            Console.WriteLine($"Your annual return rate is {annualReturn}%.");
            decimal initialAmount = ui.ReadDecimal("Enter initial investment amount: ");
            int period = ui.ReadInt("How long do you want to invest? (years): ");
            decimal monthlySubsidy = ui.ReadDecimal("How much do you want to subsidize monthly?: ");
            decimal calculatedValue = calculator.CalculatedValue(monthlySubsidy, initialAmount, annualReturn, period);
            decimal pureReturn = calculator.getPureReturnValue(monthlySubsidy, initialAmount, annualReturn, period);

            Console.WriteLine($"Total value after {period} years: {calculatedValue:C}");
            Console.WriteLine($"Pure return value of your investment is {pureReturn:C}");
        }

    }
}
