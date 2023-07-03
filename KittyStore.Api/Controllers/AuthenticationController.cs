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
        public async Task<IActionResult> Register([FromForm]RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var registerResult = await _mediator.Send(command);

            return registerResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm]LoginRequest request)
        {
            var command = _mapper.Map<LoginQuery>(request);
            var loginResult = await _mediator.Send(command);

            return loginResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors));
        }
    }
}