using ErrorOr;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.Common.Interfaces.Utils;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.OrderAggregate;
using MediatR;

namespace KittyStore.Application.Orders.Queries.GetAllUserOrders
{
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
}