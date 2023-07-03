namespace KittyStore.Application.Common.Interfaces.Services;

public interface ICurrentUserService
{
    bool TryGetUserId(out Guid result);
}