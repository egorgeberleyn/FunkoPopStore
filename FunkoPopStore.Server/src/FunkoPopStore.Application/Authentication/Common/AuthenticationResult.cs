using FunkoPopStore.Domain.UserAggregate;

namespace FunkoPopStore.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token,
    string RefreshToken);