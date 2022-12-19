using ErrorOr;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.UserAggregate;
using MediatR;

namespace KittyStore.Application.Users.AddBalanceToUser;

public class AddBalanceToUserCommandHandler : IRequestHandler<AddBalanceToUserCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public AddBalanceToUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(AddBalanceToUserCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByIdAsync(command.UserId) is not {} user)
            return Errors.User.NotFound;

        user.Balance.Replenishment(command.Amount);
        await _userRepository.UpdateUserAsync(user);

        return user;
    }
}