using ErrorOr;
using KittyStore.Application.Common.SaveChangesPostProcessor;
using KittyStore.Domain.UserAggregate;
using MediatR;

namespace KittyStore.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand : IRequest<ErrorOr<User>>, ICommand
    {
        public Guid Id { get; set; }
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string Email { get; init; } = default!;
        public BalanceCommand Balance { get; init; } = default!;
    }

    public record BalanceCommand(
        string Currency,
        decimal Amount);
}