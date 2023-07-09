using ErrorOr;
using KittyStore.Domain.UserAggregate;
using MediatR;

namespace KittyStore.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand : IRequest<ErrorOr<User>>
    {
        public Guid Id { get; set; } = default!;
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string Email { get; init; } = default!;
        public BalanceCommand Balance { get; init; } = default!;
    }

    public record BalanceCommand(
        string Currency,
        decimal Amount);
}