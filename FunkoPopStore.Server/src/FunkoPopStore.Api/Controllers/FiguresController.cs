using FunkoPopStore.Application.Figures.Queries.GetAllFigures;
using FunkoPopStore.Application.Figures.Queries.GetCat;
using FunkoPopStore.Contracts.Admin.Figures;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunkoPopStore.Api.Controllers;

[Route("/figures")]
[AllowAnonymous]
public class FiguresController : ApiController
{
    public FiguresController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await Mediator.Send(new GetAllFiguresQuery());

        return result.Match(
            figures => Ok(Mapper.Map<List<FigureResponse>>(figures)),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await Mediator.Send(new GetFigureQuery(id));

        return result.Match(
            figure => Ok(Mapper.Map<FigureResponse>(figure)),
            errors => Problem(errors));
    }
}