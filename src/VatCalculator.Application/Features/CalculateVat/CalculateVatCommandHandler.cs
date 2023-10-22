namespace VatCalculator.Application.Features.CalculateVat;

using FluentResults;
using MediatR;
using VatCalculator.Application.Interfaces.Calculation.Strategies;
using VatCalculator.Contracts.Calculation;

public class CalculateVatCommandHandler : IRequestHandler<CalculateVatCommand, Result<CalculationResponse>>
{
    private readonly ICalculationStrategyFactory _strategyFactory;
    public CalculateVatCommandHandler(ICalculationStrategyFactory strategyFactory)
    {
        _strategyFactory = strategyFactory;
    }

    public Task<Result<CalculationResponse>> Handle(CalculateVatCommand command, CancellationToken cancellationToken)
    {
        var strategyResult = _strategyFactory.GetStrategy(command);

        if (strategyResult.IsFailed)
            return Task.FromResult(strategyResult.ToResult<CalculationResponse>());

        var result = strategyResult.Value.Calculate(command);

        return Task.FromResult(Result.Ok(result));
    }
}