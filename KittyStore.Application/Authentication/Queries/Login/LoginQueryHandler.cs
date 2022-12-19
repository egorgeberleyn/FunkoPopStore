using System.Security.Cryptography;
using System.Text;
using MediatR;
using ErrorOr;
using KittyStore.Application.Authentication.Common;
using KittyStore.Application.Common.Interfaces.Authentication;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.Common.Errors;

namespace KittyStore.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
        public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
    
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            //Validate parameters
            if (await _userRepository.GetUserByEmailAsync(query.Email) is not { } user)
                return Errors.Authentication.InvalidCredentials;

            if (!VerifyPasswordHash(query.Password, user.PasswordHash, user.PasswordSalt))
                return new[] { Errors.Authentication.InvalidCredentials };

            //Generate token
            var token = _jwtTokenGenerator.GenerateToken(user);
        
            return new AuthenticationResult(
                user, 
                token);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}