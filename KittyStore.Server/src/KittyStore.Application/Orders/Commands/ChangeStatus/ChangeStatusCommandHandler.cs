using ErrorOr;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.OrderAggregate.Enums;
using MediatR;

namespace KittyStore.Application.Orders.Commands.ChangeStatus;

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