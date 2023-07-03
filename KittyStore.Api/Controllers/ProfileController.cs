using KittyStore.Application.Users.Commands.AddBalanceToUser;
using KittyStore.Contracts.Admin.Users;
using KittyStore.Contracts.Profile;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KittyStore.Api.Controllers
{
    [Route("/profile")]
    public class ProfileController : ApiController
    {
        public ProfileController(ISender mediator, IMapper mapper) : base(mediator, mapper) { }

        /// <summary>
        /// Add balance to user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> AddBalanceToUser(AddBalanceToUserRequest request)
        {
            var command = _mapper.Map<AddBalanceToUserCommand>(request);
            var result = await _mediator.Send(command);
        
            return result.Match(
                user => Ok(_mapper.Map<UserResponse>(user)),
                errors => Problem(errors));
        }
    }
}