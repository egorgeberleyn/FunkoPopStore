using FunkoPopStore.Application.Users.Commands.DeleteUser;
using FunkoPopStore.Application.Users.Commands.UpdateUser;
using FunkoPopStore.Application.Users.Queries.GetAllUsers;
using FunkoPopStore.Application.Users.Queries.GetUserById;
using FunkoPopStore.Contracts.Admin.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunkoPopStore.Api.Controllers.Admin;

[Route("/adminPanel/users")]
[Authorize(Roles = "Admin")]
public class AdminUsersController : ApiController
{
    public AdminUsersController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await Mediator.Send(new GetAllUsersQuery());

        return result.Match(
            users => Ok(Mapper.Map<List<UserResponse>>(users)),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var result = await Mediator.Send(
            new GetUserByIdQuery(id));

        return result.Match(
            user => Ok(Mapper.Map<UserResponse>(user)),
            errors => Problem(errors));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, UpdateUserRequest request)
    {
        var command = Mapper.Map<UpdateUserCommand>(request);
        var updatedResult = await Mediator.Send(command with { Id = id });

        return updatedResult.Match(
            user => Ok(Mapper.Map<UserResponse>(user)),
            errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var deletedResult = await Mediator.Send(new DeleteUserCommand(id));

        return deletedResult.Match(
            _ => Ok(),
            errors => Problem(errors));
    }
}