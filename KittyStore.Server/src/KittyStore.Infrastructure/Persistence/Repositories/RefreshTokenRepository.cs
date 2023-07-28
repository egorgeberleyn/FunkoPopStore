using KittyStore.Application.Authentication.Common;
using KittyStore.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _appDbContext;

    public RefreshTokenRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<RefreshToken?> GetTokenAsync(string token) =>
        await _appDbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);

    public void UpdateToken(RefreshToken token) =>
        _appDbContext.RefreshTokens.Update(token);
}