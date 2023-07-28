using ErrorOr;
using KittyStore.Application.Common.SaveChangesPostProcessor;
using KittyStore.Domain.UserAggregate;
using MediatR;

namespace KittyStore.Application.Users.Commands.AddBalanceToUser
{
    public record AddBalanceToUserCommand(
        decimal Amount) : IRequest<ErrorOr<User>>, ICommand;
}