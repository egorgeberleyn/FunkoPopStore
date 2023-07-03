using KittyStore.Application.Orders.Commands.CreateOrder;
using KittyStore.Application.Orders.Queries.GetAllUserOrders;
using KittyStore.Contracts.Orders;
using KittyStore.Domain.UserAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KittyStore.Api.Controllers
{
    [Route("/orders")]
    public class OrdersController : ApiController
    {
        public OrdersController(ISender mediator, IMapper mapper) : base(mediator, mapper) { }

        /// <summary>
        /// Get all of the user's orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUserOrders()
        {
            var result = await _mediator.Send(new GetAllUserOrdersQuery());

            return result.Match(
                orders => Ok(_mapper.Map<List<OrderResponse>>(orders)),
                errors => Problem(errors));
        }
    
        /// <summary>
        /// Create new order
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            var command = _mapper.Map<CreateOrderCommand>(request);
            var createdResult = await _mediator.Send(command);

            return createdResult.Match(
                order => Ok(_mapper.Map<OrderResponse>(order)),
                errors => Problem(errors));
        }
    }
}