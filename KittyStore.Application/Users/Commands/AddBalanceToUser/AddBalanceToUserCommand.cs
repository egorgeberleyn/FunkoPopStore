using ErrorOr;
using KittyStore.Domain.UserAggregate;
using MediatR;

namespace KittyStore.Application.Users.Commands.AddBalanceToUser
{
    public record AddBalanceToUserCommand(
        decimal Amount) : IRequest<ErrorOr<User>>;
}