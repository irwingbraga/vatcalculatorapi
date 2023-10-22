namespace VatCalculator.Application;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VatCalculator.Application.Common.Behaviors;
using VatCalculator.Application.Interfaces.Common;

public static class DependencyInjection
{
    public static Assembly Assembly = typeof(DependencyInjection).Assembly;

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddStrategies()
            .AddValidators()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatePipelineBehavior<,>))
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public static IServiceCollection AddStrategies(this IServiceCollection services)
    {
        return services.RegisterImplementedTypes(Assembly, ServiceLifetime.Transient, typeof(IStrategy));
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        return services.RegisterImplementedTypes(Assembly, ServiceLifetime.Transient, typeof(IValidator<>));
    }

    public static IServiceCollection RegisterImplementedTypes(
        this IServiceCollection services,
        Assembly assemblies,
        ServiceLifetime lifetime,
        params Type[] types)
    {
        return services.Scan(config =>
            config.FromAssemblies(assemblies)
                .AddClasses(filter => filter.AssignableToAny(types))
                .AsImplementedInterfaces()
                .WithLifetime(lifetime));
    }
}