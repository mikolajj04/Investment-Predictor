using System;
namespace InvestmentPredictor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IInvestmentCalculator calculator = new InvestmentCalculator();
            UserInterface ui = new UserInterface(calculator);
            ui.DisplayMenu("0.4"); //UI
            decimal annualReturn = ui.GetAnnualReturn( out MarketIndex index);
            Console.WriteLine($"Your annual return rate is {annualReturn}%.");
            decimal initialAmount = ui.ReadDecimal("Enter initial investment amount: ");
            int period = ui.ReadInt("How long do you want to invest? (years): ");
            decimal monthlySubsidy = ui.ReadDecimal("How much do you want to subsidize monthly?: ");
            var calculatorParams=new CalculatorParams(index ,monthlySubsidy, initialAmount,0, annualReturn, period);
            decimal calculatedValue = calculator.CalculatedValue(calculatorParams);
            decimal pureReturn = calculator.GetPureReturnValue(calculatorParams);
            decimal finalNetValue = calculator.TotalValueAfterTax(calculatorParams);
            Console.WriteLine($"Total value after {period} years: {calculatedValue:C}");

            //Debug Line 
            Console.WriteLine($"Pure return value of your investment is {pureReturn:C}");

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine($"Your overall total value after tax: {finalNetValue:C}");

        }

    }
}
