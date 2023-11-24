using ErrorOr;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.Common.Errors;
using FunkoPopStore.Domain.UserAggregate.Enums;
using MediatR;

namespace FunkoPopStore.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByIdAsync(command.Id) is not { } user)
            return Errors.User.NotFound;

        if (user.Role is Role.Admin)
            return Errors.User.AdminCannotBeDeleted;

        _userRepository.DeleteUser(user);
        return Unit.Value;
    }
}