using KittyStore.Domain.UserAggregate;
using KittyStore.Domain.UserAggregate.Enums;
using KittyStore.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KittyStore.Infrastructure.Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private readonly List<User> _cats = new()
    {
        User.Create("Jorge", "Admin", "admin123@gmail.com", "secret123",
            1000, Role.Admin), // Admin

        User.Create("Don", "Test Customer", "1v2goog@gmail.com", "simple",
            500, Role.Customer), // Test customer
    };


    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.Property(user => user.Id)
            .HasConversion(
                id => id.Value,
                value => new UserId(value))
            .IsRequired();

        builder.Property(c => c.FirstName).IsRequired();
        builder.Property(c => c.LastName).IsRequired();
        builder.Property(c => c.Email).IsRequired();
        builder.Property(c => c.Password).IsRequired();
        builder.Property(c => c.Role).IsRequired();
        builder.Property(c => c.Balance).IsRequired();
        builder.Property(c => c.CreatedDateTime).IsRequired();
        builder.Property(c => c.UpdatedDateTime).IsRequired();

        builder.HasData(_cats);
    }
}