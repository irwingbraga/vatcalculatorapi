namespace VatCalculator.UnitTests.Application.Calculation.Strategies;

using AutoFixture.Xunit2;
using FluentAssertions;
using VatCalculator.Application.Features.CalculateVat;
using VatCalculator.Application.Features.CalculateVat.Strategies;
using Xunit;

public class NetAmountCalculationStrategyTests
{
    [Theory]
    [AutoData]
    public void Calculate_GivenValidNetAndVatRate_ShouldCorrectlyCalculateGrossAndVat(decimal netAmount, decimal vatRate)
    {
        // Arrange
        var strategy = new NetAmountCalculationStrategy();

        var expectedVatAmount = netAmount * (vatRate / 100);
        var expectedGrossAmount = netAmount + expectedVatAmount;

        var command = new CalculateVatCommand
        {
            NetAmount = netAmount,
            VatRate = vatRate
        };

        // Act
        var result = strategy.Calculate(command);

        // Assert
        result.GrossAmount.Should().BeApproximately(expectedGrossAmount, 0.0001m);
        result.VatAmount.Should().BeApproximately(expectedVatAmount, 0.0001m);
    }
}