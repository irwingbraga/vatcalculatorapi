namespace VatCalculator.Application.Interfaces.Calculation.Strategies;

using FluentResults;
using VatCalculator.Application.Features.CalculateVat;
using VatCalculator.Application.Interfaces.Common;

public interface ICalculationStrategyFactory : IStrategy
{
    Result<ICalculationStrategy> GetStrategy(CalculateVatCommand command);
}
