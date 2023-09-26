using KittyStore.Application.Authentication.Common;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.Common.Primitives;
using KittyStore.Domain.OrderAggregate;
using KittyStore.Domain.OrderAggregate.Entities;
using KittyStore.Domain.UserAggregate;
using KittyStore.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly PublishDomainEventsInterceptor? _domainEventsInterceptor;

        public AppDbContext(DbContextOptions<AppDbContext> options,
            PublishDomainEventsInterceptor? domainEventsInterceptor = null) : base(options)
        {
            _domainEventsInterceptor = domainEventsInterceptor;
        }

        public DbSet<Cat> Cats { get; set; } = null!;
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
            base.OnConfiguring(optionsBuilder);
        }
    }
}