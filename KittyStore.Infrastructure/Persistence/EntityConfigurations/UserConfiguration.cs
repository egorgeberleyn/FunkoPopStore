using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using KittyStore.Domain.UserAggregate;
using KittyStore.Domain.UserAggregate.Enums;
using KittyStore.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KittyStore.Infrastructure.Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.Property(user => user.Id)
            .HasConversion(
                id => id.Value,
                value => new UserId(value))
            .IsRequired();

        builder.Property(e => e.Balance)
            .HasConversion(
                v => JsonSerializer.Serialize(v, null as JsonSerializerOptions),
                v => JsonSerializer.Deserialize<Balance>(v, null as JsonSerializerOptions)!);

        builder.Property(u => u.FirstName).IsRequired();
        builder.Property(u => u.LastName).IsRequired();
        builder.Property(u => u.Email).IsRequired();
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.PasswordSalt).IsRequired();
        builder.Property(u => u.Role).IsRequired();
        builder.Property(u => u.CreatedDateTime).IsRequired();
        builder.Property(u => u.UpdatedDateTime).IsRequired();

        builder.HasData(CreateTestCats());
    }

    #region Create test data
    private static IEnumerable<User> CreateTestCats()
    {
        CreateTestPasswordHash("secret123", out byte[] adminPasswordHash, out byte[] adminPasswordSalt);
        CreateTestPasswordHash("simple", out byte[] passwordHash, out byte[] passwordSalt);
        
        var cats = new List<User>()
        {
            User.Create("Jorge", "Admin", "admin123@gmail.com", adminPasswordHash, adminPasswordSalt,
                Balance.Create(Currency.Dollar, 1000), Role.Admin), // Admin (password = secret123)

            User.Create("Don", "Test Customer", "1v2goog@gmail.com", passwordHash, passwordSalt,
                Balance.Create(Currency.Dollar, 500), Role.Customer), // Test customer (password = simple)
        };
        
        void CreateTestPasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        return cats;
    }
    #endregion
}