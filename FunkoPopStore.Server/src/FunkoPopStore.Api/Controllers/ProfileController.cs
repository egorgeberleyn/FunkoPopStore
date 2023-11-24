using FunkoPopStore.Application.Users.Commands.AddBalanceToUser;
using FunkoPopStore.Contracts.Admin.Users;
using FunkoPopStore.Contracts.Profile;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FunkoPopStore.Api.Controllers;

[Route("/profile")]
public class ProfileController : ApiController
{
    public ProfileController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    /// <summary>
    /// Add balance to user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> AddBalanceToUser(AddBalanceToUserRequest request)
    {
        var command = Mapper.Map<AddBalanceToUserCommand>(request);
        var result = await Mediator.Send(command);

        return result.Match(
            user => Ok(Mapper.Map<UserResponse>(user)),
            errors => Problem(errors));
    }
}