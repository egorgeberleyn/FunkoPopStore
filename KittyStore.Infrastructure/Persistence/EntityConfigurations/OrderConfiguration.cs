using System.Text.Json;
using KittyStore.Domain.OrderAggregate;
using KittyStore.Domain.OrderAggregate.ValueObjects;
using KittyStore.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KittyStore.Infrastructure.Persistence.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.Property(ord => ord.Created).IsRequired();
        builder.Property(ord => ord.TotalPrice).IsRequired();
        
        builder.Property(e => e.Address)
            .HasConversion(
                v => JsonSerializer.Serialize(v, null as JsonSerializerOptions),
                v => JsonSerializer.Deserialize<Address>(v, null as JsonSerializerOptions)!);
        
        builder.Property(order => order.Id)
            .HasConversion(
                id => id.Value,
                value => new OrderId(value))
            .IsRequired();
        
        builder.Property(order => order.UserId)
            .HasConversion(
                id => id.Value,
                value => new UserId(value))
            .IsRequired();
        
        builder.HasMany(x => x.OrderItems)
            .WithOne(b => b.Order)
            .HasForeignKey(b => b.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}