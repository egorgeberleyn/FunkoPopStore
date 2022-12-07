using KittyStore.Domain.UserAggregate;

namespace KittyStore.Application.Authentication.Common;

public record AuthResult(
    User User,
    string Token);
