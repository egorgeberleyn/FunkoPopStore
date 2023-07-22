using KittyStore.Domain.OrderAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KittyStore.Infrastructure.Persistence.EntityConfigurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("orderItems");

        builder.HasKey(item => item.Id);

        builder.Property(ord => ord.Price).IsRequired();

        builder.Property(item => item.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(item => item.CatId)
            .IsRequired();
    }
}