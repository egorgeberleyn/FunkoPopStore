using FunkoPopStore.Application.Authentication.Commands.RefreshToken;
using FunkoPopStore.Application.Authentication.Commands.Register;
using FunkoPopStore.Application.Authentication.Queries.Login;
using FunkoPopStore.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunkoPopStore.Api.Controllers;

[Route("/auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    public AuthenticationController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = Mapper.Map<RegisterCommand>(request);
        var registerResult = await Mediator.Send(command);

        return registerResult.Match(
            authResult => Ok(Mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = Mapper.Map<LoginQuery>(request);
        var loginResult = await Mediator.Send(command);

        return loginResult.Match(
            authResult => Ok(Mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken(TokenRequest request)
    {
        var command = Mapper.Map<RefreshTokenCommand>(request);
        var result = await Mediator.Send(command);

        return result.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }
}