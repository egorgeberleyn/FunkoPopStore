using FunkoPopStore.Application.Authentication.Common;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.Common.Primitives;
using FunkoPopStore.Domain.FigureAggregate;
using FunkoPopStore.Domain.OrderAggregate;
using FunkoPopStore.Domain.OrderAggregate.Entities;
using FunkoPopStore.Domain.UserAggregate;
using FunkoPopStore.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace FunkoPopStore.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    private readonly PublishDomainEventsInterceptor? _domainEventsInterceptor;
    private readonly SlowQueryInterceptor? _slowQueryInterceptor;

    public AppDbContext(DbContextOptions<AppDbContext> options,
        PublishDomainEventsInterceptor? domainEventsInterceptor = null, 
        SlowQueryInterceptor? slowQueryInterceptor = null) : base(options)
    {
        _domainEventsInterceptor = domainEventsInterceptor;
        _slowQueryInterceptor = slowQueryInterceptor;
    }

    public DbSet<Figure> Figures { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrdersItems { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_domainEventsInterceptor != null)
            optionsBuilder.AddInterceptors(_domainEventsInterceptor);
        if (_slowQueryInterceptor != null)
            optionsBuilder.AddInterceptors(_slowQueryInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}