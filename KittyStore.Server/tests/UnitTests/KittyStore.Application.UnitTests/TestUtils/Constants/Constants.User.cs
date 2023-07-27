using KittyStore.Domain.UserAggregate.Enums;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class User
    {
        public static readonly Guid IncorrectId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
        
        public const string FirstName = "Test";
        public const string LastName = "Tester";
        public const string Email = "test@gmail.com";
        public static readonly byte[] PasswordHash = new byte[] { 1, 2, 3 };
        public static readonly byte[] PasswordSalt = new byte[] { 4, 5, 6 };
        public static readonly Balance Balance = Balance.Create(Currency.Dollar, 100);
    }
}