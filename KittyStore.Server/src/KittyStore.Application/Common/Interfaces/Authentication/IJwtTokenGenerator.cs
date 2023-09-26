using System.Security.Claims;
using KittyStore.Domain.UserAggregate;
using ErrorOr;
using KittyStore.Application.Authentication.Common;

namespace KittyStore.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<(string accessToken, string refreshToken)> GenerateTokenPairAsync(User user);

        ErrorOr<Success> ValidateRefreshToken(ClaimsPrincipal tokenInVerification, ref RefreshToken refreshToken);

        ErrorOr<ClaimsPrincipal> ValidateAccessToken(string accessToken);
    }
}