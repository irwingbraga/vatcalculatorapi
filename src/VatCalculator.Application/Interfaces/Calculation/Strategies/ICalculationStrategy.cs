namespace VatCalculator.Application.Interfaces.Calculation.Strategies;

using VatCalculator.Application.Features.CalculateVat;
using VatCalculator.Application.Interfaces.Common;
using VatCalculator.Contracts.Calculation;

public interface ICalculationStrategy : IStrategy
{
    bool CanHandle(CalculateVatCommand command);
    CalculationResponse Calculate(CalculateVatCommand command);
}
