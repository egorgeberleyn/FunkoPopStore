using ErrorOr;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.OrderAggregate;
using MediatR;

namespace KittyStore.Application.Orders.Queries.GetAllUserOrders;

public class GetAllUserOrdersQueryHandler : IRequestHandler<GetAllUserOrdersQuery, ErrorOr<List<Order>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;

    public GetAllUserOrdersQueryHandler(IOrderRepository orderRepository, IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<List<Order>>> Handle(GetAllUserOrdersQuery query, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByIdAsync(query.UserId) is not {} user)
            return Errors.User.NotFound;
        
        var orders = await _orderRepository.GetUserOrdersAsync(user.Id);

        return orders;
    }
}