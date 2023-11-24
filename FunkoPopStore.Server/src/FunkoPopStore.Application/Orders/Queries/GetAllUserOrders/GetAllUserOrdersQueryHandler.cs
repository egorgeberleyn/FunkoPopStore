using ErrorOr;
using FunkoPopStore.Application.Common.Interfaces.Authentication;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.Common.Errors;
using FunkoPopStore.Domain.OrderAggregate;
using MediatR;

namespace FunkoPopStore.Application.Orders.Queries.GetAllUserOrders;

public class GetAllUserOrdersQueryHandler : IRequestHandler<GetAllUserOrdersQuery, ErrorOr<List<Order>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetAllUserOrdersQueryHandler(IOrderRepository orderRepository, ICurrentUserService currentUserService)
    {
        _orderRepository = orderRepository;
        _currentUserService = currentUserService;
    }

    public async Task<ErrorOr<List<Order>>> Handle(GetAllUserOrdersQuery query, CancellationToken cancellationToken)
    {
        if (!_currentUserService.TryGetUserId(out var userId))
            return Errors.User.NotFound;

        var orders = await _orderRepository.GetUserOrdersAsync(userId);

        return orders;
    }
}