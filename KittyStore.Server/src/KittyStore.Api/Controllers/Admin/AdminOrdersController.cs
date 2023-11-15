using KittyStore.Application.Orders.Commands.ChangeStatus;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KittyStore.Api.Controllers.Admin;

[Route("/adminPanel/orders")]
[Authorize(Roles = "Admin")]
public class AdminOrdersController : ApiController
{
    public AdminOrdersController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPatch("{orderId:guid}")]
    public async Task<IActionResult> ChangeOrderStatus(Guid orderId,
        string status)
    {
        var result = await Mediator.Send(new ChangeStatusCommand(orderId, status));

        return result.Match(
            success => Ok(success),
            errors => Problem(errors));
    }
}