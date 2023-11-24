using FunkoPopStore.Application.Authentication.Common;

namespace FunkoPopStore.Application.Common.Interfaces.Persistence;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetTokenAsync(string token);
    void UpdateToken(RefreshToken token);
}