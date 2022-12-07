using KittyStore.Api.Common.Errors;
using KittyStore.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace KittyStore.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddMappings();
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, KittyStoreProblemDetailsFactory>();
        return services;
    }
}