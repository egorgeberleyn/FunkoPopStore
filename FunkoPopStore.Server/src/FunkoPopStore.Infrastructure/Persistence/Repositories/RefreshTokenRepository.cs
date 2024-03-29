using FunkoPopStore.Application.Authentication.Common;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FunkoPopStore.Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _appDbContext;

    public RefreshTokenRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public Task<RefreshToken?> GetTokenAsync(string token) =>
        _appDbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);

    public void UpdateToken(RefreshToken token) =>
        _appDbContext.RefreshTokens.Update(token);
}