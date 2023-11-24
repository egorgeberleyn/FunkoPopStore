using FunkoPopStore.Domain.UserAggregate;
using FunkoPopStore.Domain.UserAggregate.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FunkoPopStore.Infrastructure.Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private readonly User _admin = SeedData.CreateAdmin("Jorge", "Admin", "secret123", "admin123@gmail.com");

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.OwnsOne(user => user.Balance, b =>
        {
            b.Property(balance => balance.Currency).HasConversion(
                v => v.ToString(),
                v => (Currency)Enum.Parse(typeof(Currency), v));
            b.Property(balance => balance.Amount);
        });

        builder.OwnsOne(user => user.Balance).HasData(
            new { UserId = _admin.Id, Currency = Currency.Dollar, Amount = 1000m });

        builder.Property(user => user.Role)
            .HasConversion(
                v => v.ToString(),
                v => (Role)Enum.Parse(typeof(Role), v));

        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(60);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.PasswordSalt).IsRequired();
        builder.Property(u => u.Role).IsRequired();
        builder.Property(u => u.CreatedDateTime).IsRequired();
        builder.Property(u => u.UpdatedDateTime).IsRequired();

        builder.HasData(_admin);
    }
}