using MediatR;
using ErrorOr;
using KittyStore.Application.Authentication.Common;
using KittyStore.Application.Common.Interfaces.Authentication;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.UserAggregate;
using KittyStore.Domain.UserAggregate.Enums;

namespace KittyStore.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        //Validate email
        if (_userRepository.GetUserByEmailAsync(command.Email) is not null)
            return Errors.User.DuplicateEmail;

        //Create user and add to db
        var user = User.Create(command.FirstName, command.LastName, command.Email, 
            command.Password, 0, Role.Customer);
        await _userRepository.AddUserAsync(user);
        
        //Jwt token generate
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user,
            token);
    }
}