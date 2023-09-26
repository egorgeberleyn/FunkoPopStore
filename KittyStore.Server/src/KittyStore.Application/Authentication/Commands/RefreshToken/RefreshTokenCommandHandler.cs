using MediatR;
using ErrorOr;
using KittyStore.Application.Authentication.Common;
using KittyStore.Application.Common.Interfaces.Authentication;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.Common.Errors;

namespace KittyStore.Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RefreshTokenCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var tokenInVerification = _jwtTokenGenerator.ValidateAccessToken(request.Token);
        if (tokenInVerification.IsError)
            return tokenInVerification.FirstError;

        var storedToken = await _refreshTokenRepository.GetTokenAsync(request.RefreshToken);
        if (storedToken == null) return Errors.Authentication.InvalidRefreshToken;

        var result = _jwtTokenGenerator.ValidateRefreshToken(tokenInVerification.Value, ref storedToken);
        if (result.IsError)
            return tokenInVerification.FirstError;

        storedToken.IsUsed = true;
        _refreshTokenRepository.UpdateToken(storedToken);

        var user = await _userRepository.GetUserByIdAsync(storedToken.UserId);
        if (user is null)
            return Errors.User.NotFound;

        var tokenPair = await _jwtTokenGenerator.GenerateTokenPairAsync(user);
        return new AuthenticationResult(user, tokenPair.accessToken, tokenPair.refreshToken);
    }
}