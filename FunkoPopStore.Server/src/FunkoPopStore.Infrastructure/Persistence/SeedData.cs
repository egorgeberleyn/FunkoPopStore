using System.Security.Cryptography;
using System.Text;
using FunkoPopStore.Domain.FigureAggregate;
using FunkoPopStore.Domain.FigureAggregate.Enums;
using FunkoPopStore.Domain.UserAggregate;
using FunkoPopStore.Domain.UserAggregate.Enums;

namespace FunkoPopStore.Infrastructure.Persistence;

public static class SeedData
{
    public static List<Figure> Figures => new()
    {
        Figure.Create("Spider-Man", 223, Rarity.Common, DateTime.Parse("01.01.2012")),
        Figure.Create("Guts", 3324, Rarity.Secret, DateTime.Parse("22.10.2022")),
        Figure.Create("Gold Batman", 567, Rarity.Legend, DateTime.Parse("07.03.2021")),
    };

    public static User CreateAdmin(string firstName, string lastName, string password, string email)
    {
        var salt = RandomNumberGenerator.GetBytes(64);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password), salt, 350000, HashAlgorithmName.SHA512, 64);
        return User.Create(firstName, lastName, email, hash,
            salt, Role.Admin);
    }
}