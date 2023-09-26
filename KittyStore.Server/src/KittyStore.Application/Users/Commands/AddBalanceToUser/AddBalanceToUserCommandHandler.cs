using ErrorOr;
using KittyStore.Application.Common.Interfaces.Authentication;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.Common.Interfaces.Utils;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.UserAggregate;
using MediatR;

namespace KittyStore.Application.Users.Commands.AddBalanceToUser
{
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
}