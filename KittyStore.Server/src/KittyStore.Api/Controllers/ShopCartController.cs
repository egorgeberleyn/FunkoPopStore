using KittyStore.Application.ShopCarts.Commands.AddItem;
using KittyStore.Application.ShopCarts.Commands.RemoveItem;
using KittyStore.Application.ShopCarts.Queries.GetShopCart;
using KittyStore.Contracts.ShopCart;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KittyStore.Api.Controllers
{
    [Route("/shopCart")]
    public class ShopCartController : ApiController
    {
        public ShopCartController(ISender mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet("")]
        public async Task<IActionResult> GetShopCart()
        {
            var result = await Mediator.Send(
                new GetShopCartQuery());

            return result.Match(
                cart => Ok(Mapper.Map<ShopCartResponse>(cart)),
                errors => Problem(errors)
            );
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem(AddShopCartItemRequest request)
        {
            var command = Mapper.Map<AddShopCartItemCommand>(request);
            var result = await Mediator.Send(command);

            return result.Match(
                cart => Ok(Mapper.Map<ShopCartResponse>(cart)),
                errors => Problem(errors)
            );
        }

        [HttpDelete("items/{id:guid}")]
        public async Task<IActionResult> RemoveItem(Guid id)
        {
            var result = await Mediator.Send(new RemoveItemCommand(id));

            return result.Match(
                cart => Ok(Mapper.Map<ShopCartResponse>(cart)),
                errors => Problem(errors)
            );
        }
    }
}