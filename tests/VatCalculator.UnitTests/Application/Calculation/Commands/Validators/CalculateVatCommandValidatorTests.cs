namespace VatCalculator.UnitTests.Application.Calculation.Commands.Validators;

using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation.TestHelper;
using VatCalculator.Application.Features.CalculateVat;
using Xunit;

public class CalculateVatCommandValidatorTests
{
    private readonly CalculateVatCommandValidator _validator;
    private readonly IFixture _fixture;

    public CalculateVatCommandValidatorTests()
    {
        _validator = new CalculateVatCommandValidator();
        _fixture = new Fixture();
    }

    [Theory]
    [AutoData]
    public void TestValidate_OnlyOneAmountProvided_ShouldNotHaveValidationError(CalculateVatCommand command)
    {
        // Arrange
        command.NetAmount = null;
        command.GrossAmount = _fixture.Create<decimal?>();
        command.VatAmount = null;
        command.VatRate = 10m;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().NotContain(err => err.PropertyName == nameof(command.NetAmount) || err.PropertyName == nameof(command.GrossAmount) || err.PropertyName == nameof(command.VatAmount));
    }

    [Theory]
    [AutoData]
    public void TestValidate_MultipleAmountsProvided_ShouldHaveValidationError(CalculateVatCommand command)
    {
        // Arrange
        command.NetAmount = _fixture.Create<decimal?>();
        command.GrossAmount = _fixture.Create<decimal?>();
        command.VatRate = 10m;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(err => err.ErrorMessage == "Exactly one input (net, gross, or VAT amount) is required with a value greater than 0.");
    }

    [Theory]
    [InlineAutoData(5)]
    [InlineAutoData(25)]
    [InlineAutoData(30)]
    public void TestValidate_InvalidVatRate_ShouldHaveValidationError(decimal vatRate, CalculateVatCommand command)
    {
        // Arrange
        command.VatRate = vatRate;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(err => err.PropertyName == nameof(command.VatRate));
    }

    [Theory]
    [InlineAutoData(10)]
    [InlineAutoData(13)]
    [InlineAutoData(20)]
    public void TestValidate_ValidVatRate_ShouldNotHaveValidationError(decimal vatRate, CalculateVatCommand command)
    {
        // Arrange
        command.VatRate = vatRate;
        command.NetAmount = null;
        command.GrossAmount = _fixture.Create<decimal?>();
        command.VatAmount = null;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().NotContain(err => err.PropertyName == nameof(command.VatRate));
    }

    [Theory]
    [AutoData]
    public void TestValidate_NoAmountProvided_ShouldHaveValidationError(CalculateVatCommand command)
    {
        // Arrange
        command.GrossAmount = null;
        command.VatAmount = null;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(err => err.ErrorMessage == "Invalid VAT rate. Allowed rates are 10%, 13%, and 20%.");
    }

    [Fact]
    public void TestValidate_CommandIsNull_ShouldHaveValidationError()
    {
        // Arrange
        CalculateVatCommand? command = null;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(err => err.ErrorMessage == "The request must not be null.");
    }
}
