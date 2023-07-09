using ErrorOr;
using KittyStore.Application.Common.SaveChangesPostProcessor;
using KittyStore.Domain.UserAggregate;
using MediatR;

namespace KittyStore.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand : IRequest<ErrorOr<User>>, ICommand
    {
        public Guid Id { get; set; }
        public string? FirstName { get; init; } 
        public string? LastName { get; init; } 
        public string? Email { get; init; }
        public BalanceCommand? Balance { get; init; }
    }

    public record BalanceCommand(
        string Currency,
        decimal Amount);
}