using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.OrderAggregate;
using KittyStore.Domain.OrderAggregate.Entities;
using KittyStore.Domain.UserAggregate;
using KittyStore.Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Cat> Cats { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrdersItems { get; set; } = null!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CatConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
    }
}