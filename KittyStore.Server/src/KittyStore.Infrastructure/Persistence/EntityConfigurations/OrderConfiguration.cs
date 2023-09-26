using KittyStore.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KittyStore.Infrastructure.Persistence.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(order => order.Id);

            builder.Property(ord => ord.Created).IsRequired();
            builder.Property(ord => ord.TotalPrice).IsRequired();

            builder.OwnsOne(e => e.Address);

            builder.Property(order => order.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(order => order.UserId)
                .IsRequired();

            builder.HasMany(x => x.OrderItems)
                .WithOne(b => b.Order)
                .HasForeignKey(b => b.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}