namespace KittyStore.Application.Common.Interfaces.Utils;

public interface IPasswordService
{
    public (byte[] Hash, byte[] Salt) HashPassword(string password);
    public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
}