using FunkoPopStore.Application.Figures.Commands.CreateFigure;
using FunkoPopStore.Application.Figures.Commands.DeleteFigure;
using FunkoPopStore.Application.Figures.Commands.UpdateFigure;
using FunkoPopStore.Contracts.Admin.Figures;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunkoPopStore.Api.Controllers.Admin;

[Route("/adminPanel/figures")]
[Authorize(Roles = "Admin")]
public class AdminFiguresController : ApiController
{
    public AdminFiguresController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateFigureRequest request)
    {
        var command = Mapper.Map<CreateFigureCommand>(request);
        var createdResult = await Mediator.Send(command);

        return createdResult.Match(
            figure => Ok(Mapper.Map<FigureResponse>(figure)),
            errors => Problem(errors));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateFigureRequest request)
    {
        var command = Mapper.Map<UpdateFigureCommand>(request);
        var updatedResult = await Mediator.Send(command with { Id = id });

        return updatedResult.Match(
            figure => Ok(Mapper.Map<FigureResponse>(figure)),
            errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deletedResult = await Mediator.Send(new DeleteFigureCommand(id));

        return deletedResult.Match(
            _ => Ok(),
            errors => Problem(errors));
    }
}