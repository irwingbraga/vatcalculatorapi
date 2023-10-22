namespace VatCalculator.UnitTests.Application.Calculation.Strategies;

using AutoFixture.Xunit2;
using FluentAssertions;
using VatCalculator.Application.Features.CalculateVat;
using VatCalculator.Application.Features.CalculateVat.Strategies;

public class GrossAmountCalculationStrategyTests
{
    [Theory]
    [AutoData]
    public void Calculate_GivenValidGrossAndVatRate_ShouldCorrectlyCalculateNetAndVat(decimal grossAmount, decimal vatRate)
    {
        // Arrange
        var strategy = new GrossAmountCalculationStrategy();

        var expectedVatAmount = grossAmount - (grossAmount / (1 + (vatRate / 100)));
        var expectedNetAmount = grossAmount - expectedVatAmount;

        var command = new CalculateVatCommand
        {
            GrossAmount = grossAmount,
            VatRate = vatRate
        };

        // Act
        var result = strategy.Calculate(command);

        // Assert
        result.NetAmount.Should().BeApproximately(expectedNetAmount, 0.0001m);
        result.VatAmount.Should().BeApproximately(expectedVatAmount, 0.0001m);
    }
}

