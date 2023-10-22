namespace VatCalculator.Application.Features.CalculateVat.Strategies;

using VatCalculator.Application.Features.CalculateVat;
using VatCalculator.Application.Interfaces.Calculation.Strategies;
using VatCalculator.Contracts.Calculation;

public class GrossAmountCalculationStrategy : ICalculationStrategy
{
    public CalculationResponse Calculate(CalculateVatCommand command)
    {
        var net = command.GrossAmount.Value / (1 + command.VatRate / 100);

        return new CalculationResponse
        {
            NetAmount = net,
            VatAmount = command.GrossAmount.Value - net,
            GrossAmount = command.GrossAmount.Value
        };
    }

    public bool CanHandle(CalculateVatCommand command) => command.GrossAmount.HasValue && command.GrossAmount.Value > 0;
}
