using ErrorOr;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.UserAggregate.Enums;
using MediatR;

namespace KittyStore.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<Unit>>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetUserByIdAsync(command.Id) is not {} user)
                return Errors.User.NotFound;

            if (user.Role is Role.Admin)
                return Errors.User.AdminCannotBeDeleted;

            _userRepository.DeleteUser(user);
            return Unit.Value;
        }
    }
}