using KittyStore.Application.Authentication.Common;
using KittyStore.Application.Common.Interfaces.Authentication;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.Common.Interfaces.Utils;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.UserAggregate;
using KittyStore.Domain.UserAggregate.Enums;
using KittyStore.Domain.UserAggregate.ValueObjects;
using MediatR;
using ErrorOr;

namespace KittyStore.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordService _passwordService;

    public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, 
        IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordService = passwordService;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        //Validate email
        if (await _userRepository.GetUserByEmailAsync(command.Email) is not null)
            return Errors.User.DuplicateEmail;
        
        //Create user and add to db
        var passwordInfo = _passwordService.HashPassword(command.Password);
        var user = User.Create(command.FirstName, command.LastName, command.Email, 
            passwordInfo.Hash, passwordInfo.Salt, Balance.Create(Currency.Dollar, 0), Role.Customer);
        await _userRepository.AddUserAsync(user);
        
        //Jwt token generate
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user,
            token);
    }
}