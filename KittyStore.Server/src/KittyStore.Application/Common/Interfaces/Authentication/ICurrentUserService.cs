using KittyStore.Domain.UserAggregate;

namespace KittyStore.Application.Common.Interfaces.Authentication;

public interface ICurrentUserService
{
    bool TryGetUserId(out Guid result);
    Task<User?> GetUserAsync();
}