namespace VatCalculator.Application.Common.Behaviors;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

internal class ValidatePipelineBehavior<TRequest, TResponse> : PipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : FluentResults.ResultBase, new()
{
    private readonly IServiceProvider serviceProvider;

    public ValidatePipelineBehavior(IServiceProvider serviceProvider) => this.serviceProvider = serviceProvider;

    public override async Task<TResponse> Handle(
        TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestValidator = serviceProvider.GetService<IValidator<TRequest>>()
            ?? throw new NotImplementedException($"The request validator for the type {request.GetType().Name} was not found. Make sure it is implemented.");

        var validationResult = requestValidator.Validate(request);

        if (!validationResult.IsValid)
        {
            var result = new TResponse();

            foreach (var validationError in validationResult.Errors)
            {
                var error = new FluentResults.Error(validationError.ErrorMessage);
                error.WithMetadata("validation", "validation");
                result.Reasons.Add(error);
            }

            return result;
        }

        return await next();
    }
}
