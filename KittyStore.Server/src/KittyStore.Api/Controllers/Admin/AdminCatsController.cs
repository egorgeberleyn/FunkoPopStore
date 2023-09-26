using KittyStore.Application.Cats.Commands.CreateCat;
using KittyStore.Application.Cats.Commands.DeleteCat;
using KittyStore.Application.Cats.Commands.UpdateCat;
using KittyStore.Contracts.Admin.Cats;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KittyStore.Api.Controllers.Admin
{
    [Route("/adminPanel/cats")]
    [Authorize(Roles = "Admin")]
    public class AdminCatsController : ApiController
    {
        public AdminCatsController(ISender mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateCat(CreateCatRequest request)
        {
            var command = Mapper.Map<CreateCatCommand>(request);
            var createdResult = await Mediator.Send(command);

            return createdResult.Match(
                cat => Ok(Mapper.Map<CatResponse>(cat)),
                errors => Problem(errors));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCat(Guid id, UpdateCatRequest request)
        {
            var command = Mapper.Map<UpdateCatCommand>(request);
            var updatedResult = await Mediator.Send(command with { Id = id });

            return updatedResult.Match(
                cat => Ok(Mapper.Map<CatResponse>(cat)),
                errors => Problem(errors));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCat(Guid id)
        {
            var deletedResult = await Mediator.Send(new DeleteCatCommand(id));

            return deletedResult.Match(
                _ => Ok(),
                errors => Problem(errors));
        }
    }
}