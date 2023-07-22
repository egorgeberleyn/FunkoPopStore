using System.Security.Claims;
using KittyStore.Application.Common.Interfaces.Utils;
using KittyStore.Domain.UserAggregate;
using Microsoft.AspNetCore.Http;
using KittyStore.Application.Common.Interfaces.Persistence;

namespace KittyStore.Infrastructure.Utils;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserRepository _userRepository;

    public CurrentUserService(IHttpContextAccessor contextAccessor, IUserRepository userRepository)
    {
        _contextAccessor = contextAccessor;
        _userRepository = userRepository;
    }

    public bool TryGetUserId(out Guid result)
    {
        var stringId = _contextAccessor.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        return Guid.TryParse(stringId, out result);
    }

    public async Task<User?> GetUserAsync()
    {
        if (!TryGetUserId(out var userId))
            return null;
        return await _userRepository.GetUserByIdAsync(userId);
    }
}