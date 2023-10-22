namespace VatCalculator.Application.Features.CalculateVat.Strategies;

using VatCalculator.Application.Features.CalculateVat;
using VatCalculator.Application.Interfaces.Calculation.Strategies;
using VatCalculator.Contracts.Calculation;

public class NetAmountCalculationStrategy : ICalculationStrategy
{
    public CalculationResponse Calculate(CalculateVatCommand command)
    {
        var vat = command.NetAmount.Value * (command.VatRate / 100);

        return new CalculationResponse
        {
            NetAmount = command.NetAmount.Value,
            VatAmount = vat,
            GrossAmount = command.NetAmount.Value + vat
        };
    }

    public bool CanHandle(CalculateVatCommand command) => command.NetAmount.HasValue && command.NetAmount.Value > 0;
}
