using KittyStore.Application.Authentication.Common;

namespace KittyStore.Application.Common.Interfaces.Persistence;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetTokenAsync(string token);
    void UpdateToken(RefreshToken token);
}