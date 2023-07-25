using ErrorOr;
using KittyStore.Application.Common.SaveChangesPostProcessor;
using KittyStore.Domain.UserAggregate;
using MediatR;

namespace KittyStore.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(
        Guid Id,
        string FirstName, 
        string LastName,
        string Email,
        BalanceCommand Balance) : IRequest<ErrorOr<User>>, ICommand;
  
    public record BalanceCommand(
        string Currency,
        decimal Amount);
}