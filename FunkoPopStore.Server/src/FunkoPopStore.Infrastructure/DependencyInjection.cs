using System.Text;
using FunkoPopStore.Application.Common.Interfaces.Authentication;
using FunkoPopStore.Application.Common.Interfaces.Cache;
using FunkoPopStore.Application.Common.Interfaces.Email;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Application.Common.Interfaces.Utils;
using FunkoPopStore.Infrastructure.Authentication;
using FunkoPopStore.Infrastructure.Cache;
using FunkoPopStore.Infrastructure.Email;
using FunkoPopStore.Infrastructure.Persistence;
using FunkoPopStore.Infrastructure.Persistence.Interceptors;
using FunkoPopStore.Infrastructure.Persistence.Repositories;
using FunkoPopStore.Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace FunkoPopStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .AddAuth(configuration)
            .AddPersistence(configuration)
            .AddCache(configuration);

        services.AddHttpContextAccessor();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IPasswordService, PasswordService>();

        services.AddScoped<IEmailService, EmailService>();
        services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.SectionName));

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFigureRepository, FigureRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IAppDbContext>(factory => factory.GetRequiredService<AppDbContext>());
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection")));

        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<SlowQueryInterceptor>();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }

    private static IServiceCollection AddCache(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>
        (ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")
                                       ?? throw new InvalidOperationException("No access to redis configuration")));
        services.AddScoped<ICacheService, CacheService>();

        return services;
    }
}