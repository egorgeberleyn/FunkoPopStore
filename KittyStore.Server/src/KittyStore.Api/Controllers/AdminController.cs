using KittyStore.Application.Cats.Commands.CreateCat;
using KittyStore.Application.Cats.Commands.DeleteCat;
using KittyStore.Application.Cats.Commands.UpdateCat;
using KittyStore.Application.Users.Commands.DeleteUser;
using KittyStore.Application.Users.Commands.UpdateUser;
using KittyStore.Application.Users.Queries.GetAllUsers;
using KittyStore.Application.Users.Queries.GetUserById;
using KittyStore.Contracts.Admin.Cats;
using KittyStore.Contracts.Admin.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KittyStore.Api.Controllers
{
    [Route("/adminPanel")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ApiController
    {
        public AdminController(ISender mediator, IMapper mapper) : base(mediator, mapper) { }
            
        [HttpPost("cats")]
        public async Task<IActionResult> CreateCat(CreateCatRequest request)
        {
            var command = Mapper.Map<CreateCatCommand>(request);
            var createdResult = await Mediator.Send(command);
        
            return createdResult.Match(
                cat => Ok(Mapper.Map<CatResponse>(cat)),
                errors => Problem(errors));
        }
    
        [HttpPut("cats/{id:guid}")]
        public async Task<IActionResult> UpdateCat(Guid id, UpdateCatRequest request)
        {
            var command = Mapper.Map<UpdateCatCommand>(request);
            command.Id = id;
            var updatedResult = await Mediator.Send(command);
        
            return updatedResult.Match(
                cat => Ok(Mapper.Map<CatResponse>(cat)),
                errors => Problem(errors));
        }
    
        [HttpDelete("cats/{id:guid}")]
        public async Task<IActionResult> DeleteCat(Guid id)
        {
            var deletedResult = await Mediator.Send(new DeleteCatCommand(id));
        
            return deletedResult.Match(
                _ => Ok(),
                errors => Problem(errors));
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await Mediator.Send(new GetAllUsersQuery());

            return result.Match(
                users => Ok(Mapper.Map<List<UserResponse>>(users)),
                errors => Problem(errors));
        }
        
        [HttpGet("users/{id:guid}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await Mediator.Send(
                new GetUserByIdQuery(id));

            return result.Match(
                user => Ok(Mapper.Map<UserResponse>(user)),
                errors => Problem(errors));
        }
    
        [HttpPut("users/{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserRequest request)
        {
            var command = Mapper.Map<UpdateUserCommand>(request);
            command.Id = id;
            var updatedResult = await Mediator.Send(command);

            return updatedResult.Match(
                user => Ok(Mapper.Map<UserResponse>(user)),
                errors => Problem(errors));
        }
    
        [HttpDelete("users/{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deletedResult = await Mediator.Send(new DeleteUserCommand(id));
        
            return deletedResult.Match(
                _ => Ok(),
                errors => Problem(errors));
        }
    }
}