using System.Security.Claims;
using ErrorOr;
using FunkoPopStore.Application.Authentication.Common;
using FunkoPopStore.Domain.UserAggregate;

namespace FunkoPopStore.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    Task<(string accessToken, string refreshToken)> GenerateTokenPairAsync(User user);

    ErrorOr<Success> ValidateRefreshToken(ClaimsPrincipal tokenInVerification, ref RefreshToken refreshToken);

    ErrorOr<ClaimsPrincipal> ValidateAccessToken(string accessToken);
}