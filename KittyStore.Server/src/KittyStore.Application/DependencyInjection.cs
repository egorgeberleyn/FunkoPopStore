using System.Reflection;
using FluentValidation;
using KittyStore.Application.Common.Behaviors;
using KittyStore.Application.Common.SaveChangesPostProcessor;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;


namespace KittyStore.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(SaveChangesCommandPostProcessor<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}