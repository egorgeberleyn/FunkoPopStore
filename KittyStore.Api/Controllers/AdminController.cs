using KittyStore.Application.Cats.Commands.CreateCat;
using KittyStore.Application.Cats.Commands.DeleteCat;
using KittyStore.Application.Cats.Commands.UpdateCat;
using KittyStore.Application.Users.Commands.DeleteUser;
using KittyStore.Application.Users.Commands.UpdateUser;
using KittyStore.Application.Users.Queries.GetAllUsers;
using KittyStore.Application.Users.Queries.GetUserById;
using KittyStore.Contracts.Admin.Cats;
using KittyStore.Contracts.Admin.Users;
using KittyStore.Domain.CatAggregate.ValueObjects;
using KittyStore.Domain.UserAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KittyStore.Api.Controllers
{
    [Route("/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AdminController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    
        [HttpPost("cats")]
        public async Task<IActionResult> CreateCat(CreateCatRequest request)
        {
            var command = _mapper.Map<CreateCatCommand>(request);
            var createdResult = await _mediator.Send(command);
        
            return createdResult.Match(
                cat => Ok(_mapper.Map<CatResponse>(cat)),
                errors => Problem(errors));
        }
    
        [HttpPut("cats/{id:guid}")]
        public async Task<IActionResult> UpdateCat(Guid id, UpdateCatRequest request)
        {
            var command = _mapper.Map<UpdateCatCommand>(request);
            command.Id = new CatId(id);
            var updatedResult = await _mediator.Send(command);
        
            return updatedResult.Match(
                cat => Ok(_mapper.Map<CatResponse>(cat)),
                errors => Problem(errors));
        }
    
        [HttpDelete("cats/{id:guid}")]
        public async Task<IActionResult> DeleteCat(Guid id)
        {
            var catId = new CatId(id);
            var deletedResult = await _mediator.Send(new DeleteCatCommand(catId));
        
            return deletedResult.Match(
                _ => Ok(),
                errors => Problem(errors));
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());

            return result.Match(
                users => Ok(_mapper.Map<List<UserResponse>>(users)),
                errors => Problem(errors));
        }
        
        [HttpGet("users/{id:guid}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await _mediator.Send(
                new GetUserByIdQuery(new UserId(id)));

            return result.Match(
                user => Ok(_mapper.Map<UserResponse>(user)),
                errors => Problem(errors));
        }
    
        [HttpPut("users/{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserRequest request)
        {
            var command = _mapper.Map<UpdateUserCommand>(request);
            command.Id = new UserId(id);
            var updatedResult = await _mediator.Send(command);

            return updatedResult.Match(
                user => Ok(_mapper.Map<UserResponse>(user)),
                errors => Problem(errors));
        }
    
        [HttpDelete("users/{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var userId = new UserId(id);
            var deletedResult = await _mediator.Send(new DeleteUserCommand(userId));
        
            return deletedResult.Match(
                _ => Ok(),
                errors => Problem(errors));
        }
    }
}