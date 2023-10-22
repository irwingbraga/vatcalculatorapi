namespace VatCalculator.Api;

using Asp.Versioning;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using VatCalculator.Api.Common.Errors;
using VatCalculator.Api.Common.Mapping;


public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        services.AddSingleton<ProblemDetailsFactory, VatCalculatorProblemDetailsFactory>();
        services.AddMappings();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        return services;
    }
}