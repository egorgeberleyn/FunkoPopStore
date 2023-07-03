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
        public ShopCartController(ISender mediator, IMapper mapper) : base(mediator, mapper) { }

        [HttpGet("")]
        public async Task<IActionResult> GetShopCart(Guid userId)
        {
            var result = await _mediator.Send(
                new GetShopCartQuery(UserId.Create(userId)));

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
        public async Task<IActionResult> RemoveItem(Guid id)
        {
            var itemId = ShopCartItemId.Create(id);
            var result = await _mediator.Send(new RemoveItemCommand(itemId));

            return result.Match(
                cart => Ok(_mapper.Map<ShopCartResponse>(cart)),
                errors => Problem(errors)
            );
        }
    }
}