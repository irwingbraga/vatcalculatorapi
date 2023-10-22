namespace VatCalculator.UnitTests.Application.Calculation.Strategies;

using AutoFixture.Xunit2;
using FluentAssertions;
using VatCalculator.Application.Features.CalculateVat;
using VatCalculator.Application.Features.CalculateVat.Strategies;

public class VatAmountCalculationStrategyTests
{
    [Theory]
    [AutoData]
    public void Calculate_GivenValidVatAmountAndVatRate_ShouldCorrectlyCalculateNetAndGross(decimal vatAmount, decimal vatRate)
    {
        // Arrange
        var strategy = new VatAmountCalculationStrategy();

        var expectedNetAmount = vatAmount / (vatRate / 100);
        var expectedGrossAmount = expectedNetAmount + vatAmount;

        var command = new CalculateVatCommand
        {
            VatAmount = vatAmount,
            VatRate = vatRate
        };

        // Act
        var result = strategy.Calculate(command);

        // Assert
        result.NetAmount.Should().BeApproximately(expectedNetAmount, 0.0001m);
        result.GrossAmount.Should().BeApproximately(expectedGrossAmount, 0.0001m);
    }
}

