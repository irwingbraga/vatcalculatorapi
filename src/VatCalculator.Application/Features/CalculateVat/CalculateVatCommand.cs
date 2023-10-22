namespace VatCalculator.Application.Features.CalculateVat;

using FluentResults;
using MediatR;
using VatCalculator.Contracts.Calculation;

public class CalculateVatCommand : IRequest<Result<CalculationResponse>>
{
    public decimal? NetAmount { get; set; }
    public decimal? GrossAmount { get; set; }
    public decimal? VatAmount { get; set; }
    public decimal VatRate { get; set; }
}
