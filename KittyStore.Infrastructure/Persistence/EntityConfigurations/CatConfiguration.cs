using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.CatAggregate.Enums;
using KittyStore.Domain.CatAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KittyStore.Infrastructure.Persistence.EntityConfigurations
{
    public class CatConfiguration : IEntityTypeConfiguration<Cat>
    {
        public void Configure(EntityTypeBuilder<Cat> builder)
        {
            builder.ToTable("cats");

            builder.HasKey(cat => cat.Id);
        
            builder.Property(cat => cat.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => CatId.Create(value))
                .IsRequired();
            
            builder.Property(cat => cat.Age).IsRequired();
            
            builder.Property(cat => cat.Breed).IsRequired().HasMaxLength(30);
            
            builder.Property(cat => cat.Color).IsRequired().HasMaxLength(30);
            
            builder.Property(cat => cat.Name).IsRequired().HasMaxLength(30);
            
            builder.Property(cat => cat.Price).IsRequired();
            
            builder.Property(e => e.Gender)
                .HasConversion(
                    v => v.ToString(),
                    v => (CatGender)Enum.Parse(typeof(CatGender), v))
                .IsRequired();

            builder.HasData(SeedData.Cats);
        }
    }
}