using KittyStore.Domain.UserAggregate;
using KittyStore.Domain.UserAggregate.Enums;
using KittyStore.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace KittyStore.Infrastructure.Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            
            builder.HasKey(user => user.Id);
        
            builder.Property(user => user.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value))
                .IsRequired();

            builder.Property(e => e.Balance)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Balance>(v)!);
            
            builder.Property(e => e.Role)
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

            builder.HasData(
               SeedData.CreateAdmin("Jorge", "Admin", "secret123", "admin123@gmail.com"));
        }
    }
}