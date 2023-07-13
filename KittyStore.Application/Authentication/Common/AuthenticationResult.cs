using KittyStore.Domain.UserAggregate;

namespace KittyStore.Application.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token,
        string RefreshToken);
}
