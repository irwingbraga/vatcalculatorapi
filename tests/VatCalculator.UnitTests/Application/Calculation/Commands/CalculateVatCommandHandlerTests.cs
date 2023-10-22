namespace VatCalculator.UnitTests.Application.Calculation.Commands;

using AutoFixture.Xunit2;
using FluentAssertions;
using FluentResults;
using NSubstitute;
using System.Threading.Tasks;
using VatCalculator.Application.Features.CalculateVat;
using VatCalculator.Application.Interfaces.Calculation.Strategies;
using VatCalculator.Contracts.Calculation;
using Xunit;

public class CalculateVatCommandHandlerTests
{
    private readonly ICalculationStrategyFactory _strategyFactory;
    private readonly CalculateVatCommandHandler _handler;

    public CalculateVatCommandHandlerTests()
    {
        _strategyFactory = Substitute.For<ICalculationStrategyFactory>();
        _handler = new CalculateVatCommandHandler(_strategyFactory);
    }

    [Theory]
    [AutoData]
    public async Task Handle_GivenValidCommand_ShouldReturnSuccessResult(CalculateVatCommand command, CalculationResponse calculationResponse)
    {
        // Arrange
        var mockStrategy = Substitute.For<ICalculationStrategy>();
        mockStrategy.Calculate(command).Returns(calculationResponse);
        _strategyFactory.GetStrategy(command).Returns(Result.Ok(mockStrategy));

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(calculationResponse);
    }

    [Theory]
    [AutoData]
    public async Task Handle_WhenStrategyCannotBeDetermined_ShouldReturnFailureResult(CalculateVatCommand command)
    {
        // Arrange
        _strategyFactory.GetStrategy(command).Returns(Result.Fail<ICalculationStrategy>("Strategy not determined."));

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e.Message == "Strategy not determined.");
    }
}
