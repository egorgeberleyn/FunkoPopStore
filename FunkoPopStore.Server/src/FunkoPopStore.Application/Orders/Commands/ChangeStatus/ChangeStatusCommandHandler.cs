using ErrorOr;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.Common.Errors;
using FunkoPopStore.Domain.OrderAggregate.Enums;
using MediatR;

namespace FunkoPopStore.Application.Orders.Commands.ChangeStatus;

public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand, ErrorOr<Success>>
{
    private readonly IOrderRepository _orderRepository;

    public ChangeStatusCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<Success>> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindOrderByIdAsync(request.Id);
        if (order is null)
            return Errors.Order.NotFound;

        var status = (OrderStatus)Enum.Parse(typeof(OrderStatus), request.Status);
        order.ChangeStatus(status);
        
        return new Success();
    }
}