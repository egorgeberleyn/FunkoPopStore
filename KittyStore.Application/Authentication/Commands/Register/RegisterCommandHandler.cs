using System.Security.Cryptography;
using System.Text;
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
        if (await _userRepository.GetUserByEmailAsync(command.Email) is not null)
            return Errors.User.DuplicateEmail;
        
        //Create user and add to db
        CreatePasswordHash(command.Password, out byte[] passwordHash, out byte[] passwordSalt);
        var user = User.Create(command.FirstName, command.LastName, command.Email, 
            passwordHash, passwordSalt, 0, Role.Customer);
        await _userRepository.AddUserAsync(user);
        
        //Jwt token generate
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user,
            token);
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}