using ErrorOr;
using FunkoPopStore.Application.Common.SaveChangesPostProcessor;
using FunkoPopStore.Domain.UserAggregate;
using MediatR;

namespace FunkoPopStore.Application.Users.Commands.AddBalanceToUser;

public record AddBalanceToUserCommand(
    decimal Amount) : IRequest<ErrorOr<User>>, ICommand;