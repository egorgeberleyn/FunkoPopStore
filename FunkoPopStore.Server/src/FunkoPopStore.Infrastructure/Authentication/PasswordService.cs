using System.Security.Cryptography;
using System.Text;
using FunkoPopStore.Application.Common.Interfaces.Authentication;

namespace FunkoPopStore.Infrastructure.Authentication;

public class PasswordService : IPasswordService
{
    private const int KeySize = 64;
    private const int Iterations = 350000;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;

    public (byte[] Hash, byte[] Salt) HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(KeySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password), salt, Iterations, HashAlgorithm, KeySize);
        return (hash, salt);
    }

    public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, passwordSalt, Iterations, HashAlgorithm, KeySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, passwordHash);
    }
}