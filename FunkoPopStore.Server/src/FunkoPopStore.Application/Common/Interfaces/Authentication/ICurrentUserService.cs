using FunkoPopStore.Domain.UserAggregate;

namespace FunkoPopStore.Application.Common.Interfaces.Authentication;

public interface ICurrentUserService
{
    bool TryGetUserId(out Guid result);
    Task<User?> GetUserAsync();
}