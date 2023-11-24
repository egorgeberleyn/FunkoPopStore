using ErrorOr;
using FunkoPopStore.Application.Common.SaveChangesPostProcessor;
using FunkoPopStore.Domain.UserAggregate;
using MediatR;

namespace FunkoPopStore.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    BalanceCommand Balance) : IRequest<ErrorOr<User>>, ICommand;

public record BalanceCommand(
    string Currency,
    decimal Amount);