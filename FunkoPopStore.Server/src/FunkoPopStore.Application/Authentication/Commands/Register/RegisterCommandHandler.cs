using ErrorOr;
using FunkoPopStore.Application.Authentication.Common;
using FunkoPopStore.Application.Common.Interfaces.Authentication;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.Common.Errors;
using FunkoPopStore.Domain.UserAggregate;
using FunkoPopStore.Domain.UserAggregate.Enums;
using FunkoPopStore.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace FunkoPopStore.Application.Authentication.Commands.Register;

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

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command,
        CancellationToken cancellationToken)
    {
        //Validate email
        if (await _userRepository.GetUserByEmailAsync(command.Email) is not null)
            return Errors.User.DuplicateEmail;

        //Create user and add to db
        var passwordInfo = _passwordService.HashPassword(command.Password);
        var user = User.Create(command.FirstName, command.LastName, command.Email,
            passwordInfo.Hash, passwordInfo.Salt, Role.Customer, Balance.Create(Currency.Dollar, 0));
        await _userRepository.AddUserAsync(user);

        //Jwt token generate
        var tokenPair = await _jwtTokenGenerator.GenerateTokenPairAsync(user);

        return new AuthenticationResult(
            user,
            tokenPair.accessToken,
            tokenPair.refreshToken);
    }
}