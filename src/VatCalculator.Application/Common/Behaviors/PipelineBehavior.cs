namespace VatCalculator.Application.Common.Behaviors;

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

internal abstract class PipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public bool ShouldExecuteBehavior { get; set; } = true;

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        => ShouldExecuteBehavior
            ? Handle(request, next, cancellationToken)
            : next();

    public abstract Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken);
}

internal abstract class PipelineBehavior<TRequest, TResponse, TRequiredAttribute> : PipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TRequiredAttribute : Attribute
{
    public TRequiredAttribute RequiredAttribute { get; }

    protected PipelineBehavior(IServiceProvider provider)
    {
        var handler = provider.GetRequiredService<IRequestHandler<TRequest, TResponse>>().GetType();
        RequiredAttribute = handler.GetCustomAttribute<TRequiredAttribute>();
        ShouldExecuteBehavior = RequiredAttribute != null;
    }
}
