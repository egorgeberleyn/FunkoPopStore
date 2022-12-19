using KittyStore.Application.ShopCarts.Commands.AddItem;
using KittyStore.Application.ShopCarts.Commands.RemoveItem;
using KittyStore.Application.ShopCarts.Queries.GetShopCart;
using KittyStore.Contracts.ShopCart;
using KittyStore.Domain.ShopCartAggregate.ValueObjects;
using KittyStore.Domain.UserAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KittyStore.Api.Controllers
{
    [Route("/shopCart")]
    public class ShopCartController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ShopCartController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetShopCart(Guid userId)
        {
            var result = await _mediator.Send(
                new GetShopCartQuery(new UserId(userId)));

            return result.Match(
                cart => Ok(_mapper.Map<ShopCartResponse>(cart)),
                errors => Problem(errors)
            );
        }
    
        [HttpPost("items")]
        public async Task<IActionResult> AddItem(AddShopCartItemRequest request)
        {
            var command = _mapper.Map<AddShopCartItemCommand>(request);
            var result = await _mediator.Send(command);

            return result.Match(
                cart => Ok(_mapper.Map<ShopCartResponse>(cart)),
                errors => Problem(errors)
            );
        }
    
        [HttpDelete("items/{id:guid}")]
        public async Task<IActionResult> RemoveItem(Guid id, Guid userId)
        {
            var itemId = new ShopCartItemId(id);
            var result = await _mediator.Send(
                new RemoveItemCommand(itemId, new UserId(userId)));

            return result.Match(
                cart => Ok(_mapper.Map<ShopCartResponse>(cart)),
                errors => Problem(errors)
            );
        }
    }
}