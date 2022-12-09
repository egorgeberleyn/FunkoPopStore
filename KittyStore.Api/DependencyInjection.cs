using KittyStore.Api.Common.Errors;
using KittyStore.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace KittyStore.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            
            options.OperationFilter<SecurityRequirementsOperationFilter>();
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Kitty Store API", Version = "v1" });   
        });
        services.AddMappings();
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, KittyStoreProblemDetailsFactory>();
        return services;
    }
}