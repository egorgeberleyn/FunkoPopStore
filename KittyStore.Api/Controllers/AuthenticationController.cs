using KittyStore.Application.Authentication.Commands;
using KittyStore.Application.Authentication.Commands.Register;
using KittyStore.Application.Authentication.Queries.Login;
using KittyStore.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KittyStore.Api.Controllers
{
    [Route("/auth")]
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        public AuthenticationController(ISender mediator, IMapper mapper): base(mediator, mapper) { }

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
    }
}