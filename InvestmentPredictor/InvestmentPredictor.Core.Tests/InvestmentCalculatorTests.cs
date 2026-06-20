using Xunit;
using FluentAssertions;
using InvestmentPredictor.Core;



namespace InvestmentPredictor.Core.Tests
{
    public class InvestmentCalculatorTests
    {

      
        [Fact]
        public void CalculateTotalValue_ShouldCalculateCorrectly_ForPostnumerandoModel()
        {
            //ARANGE
            var calculator = new InvestmentCalculator();
            var parameters = new CalculatorParams(chosenIndex: MarketIndex.Custom,
                monthlySubsidy: 100m,
                initialAmount: 1000m,
                customAnnualReturn: 12m,
                annualReturn: 12m, 
                period: 1
                );

            decimal expectedValue = 2395.07533145166m; 
            //ACT
            decimal actualResult = calculator.CalculateTotalValue(parameters);
            //ASSERT
            actualResult.Should().BeApproximately(expectedValue, 0.0001m);

        }
        [Fact]
        public void CalculateTotalValue_ShouldThrowException_WhenInitialAmountIsNegative()
        {
            // ARRANGE
            var calculator = new InvestmentCalculator();
            var invalidParameters = new CalculatorParams(
                chosenIndex: MarketIndex.Custom,
                monthlySubsidy: 100m,
                initialAmount: -500m, 
                customAnnualReturn: 5m,
                annualReturn: 5m,
                period: 5
            );

            // ACT
           
            Action act = () => calculator.CalculateTotalValue(invalidParameters);

            // ASSERT
            act.Should().Throw<ArgumentOutOfRangeException>()
               .WithMessage("*Initial amount cannot be negative.*");
           
        }

        [Fact]
        public void CalculateTaxValue_ShouldReturnCorrectBelkaTaxValue()
        {
            //ARRANGE
            var calculator = new InvestmentCalculator();
            var parameters = new CalculatorParams(
                chosenIndex: MarketIndex.Custom,
                monthlySubsidy: 100m,
                initialAmount: 1000m,
                customAnnualReturn: 12m,
                annualReturn: 12m,
                period: 1
            );
            decimal expectedTax = 37.0643129758167m;

            //ACT
            decimal actualTax = calculator.CalculateTaxValue(parameters);
            //ASSERT
            actualTax.Should().BeApproximately(expectedTax, 0001m);
            
        }
        [Fact]
            public void TotalValueAfterTax_ShouldSubtractTaxFromTotalValue()
            {
            //ARRANGE
            var calculator = new InvestmentCalculator();
            var parameters = new CalculatorParams(
                chosenIndex: MarketIndex.Custom,
                monthlySubsidy: 100m,
                initialAmount: 1000m,
                customAnnualReturn: 12m,
                annualReturn: 12m,
                period: 1
            );
            decimal expectedTotalAfterTax = 2358.01101847585m;

            //ACT
            decimal actualTotalAfterTax = calculator.TotalValueAfterTax(parameters);

            //ASSERT
            actualTotalAfterTax.Should().BeApproximately(expectedTotalAfterTax, 0.0001m);
            }
        [Fact]
        public void GetYearlyProjection_ShouldReturnCorrectListSize_AndPreserveInitialAmount()
        {
            // ARRANGE
            var calculator = new InvestmentCalculator();
            int testPeriod = 5;
            var parameters = new CalculatorParams(
                chosenIndex: MarketIndex.Custom,
                monthlySubsidy: 0m,
                initialAmount: 5000m,
                customAnnualReturn: 0m,
                annualReturn: 0m,
                period: testPeriod
            );

            // ACT
            List<decimal> projection = calculator.GetYearlyProjection(parameters);

            // ASSERT
            projection.Should().HaveCount(testPeriod + 1);
            projection.First().Should().Be(5000m); 
            projection.Last().Should().Be(5000m);  
        }

        [Fact]
        public void CalculateTotalValue_ShouldThrowException_WhenPeriodIsZeroOrNegative()
        {
            // ARRANGE
            var calculator = new InvestmentCalculator();
            var invalidParameters = new CalculatorParams(
                chosenIndex: MarketIndex.Custom,
                monthlySubsidy: 100m,
                initialAmount: 1000m,
                customAnnualReturn: 5m,
                annualReturn: 5m,
                period: 0 
            );

            // ACT
            Action act = () => calculator.CalculateTotalValue(invalidParameters);

            // ASSERT
            act.Should().Throw<ArgumentOutOfRangeException>()
               .WithMessage("*Period must be greater than zero.*");
        }

        [Theory]
        [InlineData(MarketIndex.SP500, 10.4)]
        [InlineData(MarketIndex.Nasdaq100, 14.4)]
        [InlineData(MarketIndex.WIG20, 4.8)]
        public void GetIndexAnnualReturn_ShouldReturnCorrectStaticRates(MarketIndex index, decimal expectedRate)
        {
            // ARRANGE
            var calculator = new InvestmentCalculator();

            // ACT
            decimal actualRate = calculator.GetIndexAnnualReturn(index);

            // ASSERT
            actualRate.Should().Be(expectedRate);
        }

        [Fact]
        public void GetPureReturnValue_ShouldReturnCorrectProfit_BySubtractingInvestedCapital()
        {
            // 1. ARRANGE
            var calculator = new InvestmentCalculator();
            var parameters = new CalculatorParams(
                chosenIndex: MarketIndex.Custom,
                monthlySubsidy: 100m,
                initialAmount: 1000m,
                customAnnualReturn: 12m,
                annualReturn: 12m,
                period: 1
            );

            
            decimal expectedPureProfit = 195.07533145166m;

            // 2. ACT
            decimal actualPureProfit = calculator.GetPureReturnValue(parameters);

            // 3. ASSERT
            actualPureProfit.Should().BeApproximately(expectedPureProfit, 0.0001m);
        }
    }
}