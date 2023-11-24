using FunkoPopStore.Domain.FigureAggregate;
using FunkoPopStore.Domain.FigureAggregate.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FunkoPopStore.Infrastructure.Persistence.EntityConfigurations;

public class FigureConfiguration : IEntityTypeConfiguration<Figure>
{
    public void Configure(EntityTypeBuilder<Figure> builder)
    {
        builder.ToTable("figures");

        builder.HasKey(cat => cat.Id);

        builder.Property(cat => cat.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(cat => cat.ProductionYear).IsRequired();
        
        builder.Property(cat => cat.Name).IsRequired().HasMaxLength(30);

        builder.Property(cat => cat.Price).IsRequired();

        builder.Property(e => e.Rarity)
            .HasConversion(
                v => v.ToString(),
                v => (Rarity)Enum.Parse(typeof(Rarity), v))
            .IsRequired();

        builder.HasData(SeedData.Figures);
    }
}