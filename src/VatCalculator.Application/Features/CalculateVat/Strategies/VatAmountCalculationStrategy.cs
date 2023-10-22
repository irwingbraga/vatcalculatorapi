namespace VatCalculator.Application.Features.CalculateVat.Strategies;

using VatCalculator.Application.Features.CalculateVat;
using VatCalculator.Application.Interfaces.Calculation.Strategies;
using VatCalculator.Contracts.Calculation;

public class VatAmountCalculationStrategy : ICalculationStrategy
{
    public CalculationResponse Calculate(CalculateVatCommand command)
    {
        var net = command.VatAmount.Value / (command.VatRate / 100);

        return new CalculationResponse
        {
            NetAmount = net,
            VatAmount = command.VatAmount.Value,
            GrossAmount = net + command.VatAmount.Value
        };
    }

    public bool CanHandle(CalculateVatCommand command) => command.VatAmount.HasValue && command.VatAmount.Value > 0;
}
