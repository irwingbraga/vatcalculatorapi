namespace VatCalculator.Api.Common.Mapping;

using Mapster;
using VatCalculator.Application.Features.CalculateVat;
using VatCalculator.Contracts.Calculation;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CalculationRequest, CalculateVatCommand>();
    }
}