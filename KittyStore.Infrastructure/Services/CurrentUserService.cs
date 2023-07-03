using System.Security.Claims;
using KittyStore.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace KittyStore.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public bool TryGetUserId(out Guid result)
    {
        var stringId = _contextAccessor.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        return Guid.TryParse(stringId, out result);
    }
}