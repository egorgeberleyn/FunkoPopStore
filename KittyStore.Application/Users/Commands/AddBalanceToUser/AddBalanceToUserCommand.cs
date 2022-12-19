using ErrorOr;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.UserAggregate;
using KittyStore.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace KittyStore.Application.Users.AddBalanceToUser;

public record AddBalanceToUserCommand(
    UserId UserId,
    decimal Amount) : IRequest<ErrorOr<User>>;