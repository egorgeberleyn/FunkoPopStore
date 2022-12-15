using MediatR;
using ErrorOr;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.UserAggregate;

namespace KittyStore.Application.Users.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByIdAsync(command.Id) is not {} user)
            return Errors.User.NotFound;
        
        var updateUser = user.Update(command.FirstName, command.LastName, command.Email, command.Balance);
        await _userRepository.UpdateUserAsync(updateUser);
        
        return updateUser;
    }
    
    
}