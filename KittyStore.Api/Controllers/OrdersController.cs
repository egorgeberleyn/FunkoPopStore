using KittyStore.Application.Orders.Commands.CreateOrder;
using KittyStore.Application.Orders.Queries.GetAllUserOrders;
using KittyStore.Contracts.Orders;
using KittyStore.Domain.UserAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KittyStore.Api.Controllers;

[Route("orders")]
public class OrdersController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public OrdersController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUserOrders(Guid userId)
    {
        var result = await _mediator.Send(new GetAllUserOrdersQuery(
            new UserId(userId)));

        return result.Match(
            orders => Ok(_mapper.Map<List<OrderResponse>>(orders)),
            errors => Problem(errors));
    }
    
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