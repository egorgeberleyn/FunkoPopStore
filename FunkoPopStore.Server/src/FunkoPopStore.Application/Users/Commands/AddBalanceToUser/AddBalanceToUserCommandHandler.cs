using ErrorOr;
using FunkoPopStore.Application.Common.Interfaces.Authentication;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.Common.Errors;
using FunkoPopStore.Domain.UserAggregate;
using MediatR;

namespace FunkoPopStore.Application.Users.Commands.AddBalanceToUser;

public class AddBalanceToUserCommandHandler : IRequestHandler<AddBalanceToUserCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public AddBalanceToUserCommandHandler(IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<ErrorOr<User>> Handle(AddBalanceToUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _currentUserService.GetUserAsync();
        if (user is null)
            return Errors.User.NotFound;

        user.Balance?.Replenishment(command.Amount);
        _userRepository.UpdateUser(user);
        return user;
    }
}