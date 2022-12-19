using KittyStore.Domain.UserAggregate;

namespace KittyStore.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}