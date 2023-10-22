namespace VatCalculator.Application.Features.CalculateVat;

using FluentValidation;
using FluentValidation.Results;

public class CalculateVatCommandValidator : AbstractValidator<CalculateVatCommand>
{
    public CalculateVatCommandValidator()
    {
        RuleFor(x => x).Must(x => new[]
            {
                x.NetAmount.HasValue && x.NetAmount.Value > 0,
                x.GrossAmount.HasValue && x.GrossAmount.Value > 0,
                x.VatAmount.HasValue && x.VatAmount.Value > 0
            }.Count(value => value) == 1)
        .WithMessage("Exactly one input (net, gross, or VAT amount) is required with a value greater than 0.");

        RuleFor(x => x.VatRate)
            .NotEmpty().WithMessage("VAT rate is required.")
            .Must(IsValidVatRate).WithMessage("Invalid VAT rate. Allowed rates are 10%, 13%, and 20%.");
    }

    private bool IsValidVatRate(decimal vatRate)
        => vatRate == 10m || vatRate == 13m || vatRate == 20m;

    protected override bool PreValidate(ValidationContext<CalculateVatCommand> context, ValidationResult result)
    {
        if (context.InstanceToValidate == null)
        {
            result.Errors.Add(new ValidationFailure("", "The request must not be null."));
            return false;
        }
        return true;
    }

}
