using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.OrderAggregate;
using KittyStore.Domain.OrderAggregate.Entities;
using KittyStore.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Cat> Cats { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrdersItems { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}