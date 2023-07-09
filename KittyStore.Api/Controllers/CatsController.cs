using KittyStore.Application.Cats.Queries.GetAllCats;
using KittyStore.Application.Cats.Queries.GetCat;
using KittyStore.Contracts.Admin.Cats;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KittyStore.Api.Controllers
{
    [Route("/cats")]
    [AllowAnonymous]
    public class CatsController : ApiController
    {
        public CatsController(ISender mediator, IMapper mapper) : base(mediator, mapper) { }

        [HttpGet]
        public async Task<IActionResult> GetAllCats()
        {
            var result = await Mediator.Send(new GetAllCatsQuery());

            return result.Match(
                cats => Ok(Mapper.Map<List<CatResponse>>(cats)),
                errors => Problem(errors));
        }
    
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCat(Guid id)
        {
            var result = await Mediator.Send(new GetCatQuery(id));
        
            return result.Match(
                cat => Ok(Mapper.Map<CatResponse>(cat)),
                errors => Problem(errors));
        }
    }
}