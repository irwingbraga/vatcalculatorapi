namespace VatCalculator.Application.Features.CalculateVat.Strategies;

using FluentResults;
using VatCalculator.Application.Interfaces.Calculation.Strategies;

public class CalculationStrategyFactory : ICalculationStrategyFactory
{
    private readonly IEnumerable<ICalculationStrategy> _strategies;
    public CalculationStrategyFactory(IEnumerable<ICalculationStrategy> strategies)
    {
        _strategies = strategies;
    }
    public Result<ICalculationStrategy> GetStrategy(CalculateVatCommand command)
    {
        var strategy = _strategies.SingleOrDefault(s => s.CanHandle(command));

        if (strategy == null)
        {
            return Result.Fail("Invalid calculation strategy.")
                .WithError("No suitable strategy was found for the given parameters.");
        }

        return Result.Ok(strategy);
    }
}
