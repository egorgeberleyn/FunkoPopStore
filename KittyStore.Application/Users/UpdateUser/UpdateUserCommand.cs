using ErrorOr;
using KittyStore.Domain.UserAggregate;
using KittyStore.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace KittyStore.Application.Users.UpdateUser;

public record UpdateUserCommand(
    UserId Id,
    string FirstName,
    string LastName,
    string Email,
    decimal Balance) : IRequest<ErrorOr<User>>;