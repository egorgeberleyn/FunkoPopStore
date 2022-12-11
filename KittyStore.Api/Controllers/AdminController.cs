using KittyStore.Application.Cats.Commands.CreateCat;
using KittyStore.Application.Cats.Commands.DeleteCat;
using KittyStore.Application.Cats.Commands.UpdateCat;
using KittyStore.Contracts.Cats;
using KittyStore.Domain.CatAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KittyStore.Api.Controllers;

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
    
    [HttpPost("createCat")]
    public async Task<IActionResult> CreateCat(CreateCatRequest request)
    {
        var command = _mapper.Map<CreateCatCommand>(request);
        var createResult = await _mediator.Send(command);
        
        return createResult.Match(
            result => Ok(_mapper.Map<CatResponse>(result)),
            errors => Problem(errors));
    }
    
    [HttpPut("updateCat")]
    public async Task<IActionResult> UpdateCat(UpdateCatRequest request)
    {
        var command = _mapper.Map<UpdateCatCommand>(request);
        var updateResult = await _mediator.Send(command);
        
        return updateResult.Match(
            result => Ok(_mapper.Map<CatResponse>(result)),
            errors => Problem(errors));
    }
    
    [HttpDelete("deleteCat/{id:guid}")]
    public async Task<IActionResult> DeleteCat(Guid id)
    {
        var catId = new CatId(id);
        var deleteResult = await _mediator.Send(new DeleteCatCommand(catId));
        
        return deleteResult.Match(
            _ => Ok(),
            errors => Problem(errors));
    }
}