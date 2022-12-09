using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.CatAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KittyStore.Infrastructure.Persistence.EntityConfigurations;

public class CatConfiguration : IEntityTypeConfiguration<Cat>
{
    private readonly List<Cat> _cats = new()
    {
        //Test set of cats
        
        Cat.Create("Boob", 5, "black", "britain", 70),
        Cat.Create("Gail", 5, "white", "maine coon", 50),
        Cat.Create("Neegus", 5, "ginger", "egypt", 20),
        Cat.Create("Sunny", 5, "gray", "german", 88),
    };


    public void Configure(EntityTypeBuilder<Cat> builder)
    {
        builder.ToTable("cats");
        
        builder.Property(cat => cat.Id)
            .HasConversion(
                id => id.Value,
                value => new CatId(value))
            .IsRequired();

        builder.Property(c => c.Age).IsRequired();
        builder.Property(c => c.Breed).IsRequired();
        builder.Property(c => c.Color).IsRequired();
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Price).IsRequired();

        builder.HasData(_cats);
    }
}