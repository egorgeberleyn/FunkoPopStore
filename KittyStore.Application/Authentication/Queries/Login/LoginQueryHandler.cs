using MediatR;
using ErrorOr;
using KittyStore.Application.Authentication.Common;
using KittyStore.Application.Common.Interfaces.Authentication;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.Common.Interfaces.Utils;
using KittyStore.Domain.Common.Errors;

namespace KittyStore.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordService _passwordService;

        public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator,
            IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordService = passwordService;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            //Validate parameters
            if (await _userRepository.GetUserByEmailAsync(query.Email) is not { } user)
                return Errors.Authentication.InvalidCredentials;

            var isSuccess = _passwordService.VerifyPassword(query.Password, user.PasswordHash, user.PasswordSalt);
            if (!isSuccess)
                return new[] { Errors.Authentication.InvalidCredentials };

            //Generate token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}