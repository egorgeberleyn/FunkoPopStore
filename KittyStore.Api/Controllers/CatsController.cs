﻿using KittyStore.Application.Cats.Queries.GetAllCats;
using KittyStore.Application.Cats.Queries.GetCat;
using KittyStore.Contracts.Admin.Cats;
using KittyStore.Domain.CatAggregate.ValueObjects;
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
            var result = await _mediator.Send(new GetAllCatsQuery());

            return result.Match(
                cats => Ok(_mapper.Map<List<CatResponse>>(cats)),
                errors => Problem(errors));
        }
    
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCat(Guid id)
        {
            var catId = CatId.Create(id);
            var result = await _mediator.Send(new GetCatQuery(catId));
        
            return result.Match(
                cat => Ok(_mapper.Map<CatResponse>(cat)),
                errors => Problem(errors));
        }
    }
}