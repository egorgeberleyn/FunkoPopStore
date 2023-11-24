using ErrorOr;
using FunkoPopStore.Application.Common.Interfaces.Authentication;
using FunkoPopStore.Application.Common.Interfaces.Cache;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.Common.Errors;
using FunkoPopStore.Domain.OrderAggregate;
using FunkoPopStore.Domain.OrderAggregate.Entities;
using FunkoPopStore.Domain.OrderAggregate.ValueObjects;
using FunkoPopStore.Domain.ShopCartAggregate;
using MediatR;

namespace FunkoPopStore.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<Order>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICacheService _cacheService;
    private readonly ICurrentUserService _currentUserService;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, ICacheService cacheService,
        IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _orderRepository = orderRepository;
        _cacheService = cacheService;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<ErrorOr<Order>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var user = await _currentUserService.GetUserAsync();
        if (user is null)
            return Errors.User.NotFound;

        //Get shopCart
        var cart = await _cacheService.GetDataAsync<ShopCart>(user.Id.ToString());

        //Order calculation
        if (cart is null || cart.ShopCartItems.Count == 0)
            return Errors.ShopCart.ShopCartEmpty;
        var totalPrice = cart.TotalPrice;
        if (totalPrice > user.Balance?.Amount)
            return Errors.User.NotEnoughBalance;
        user.Balance?.Debit(totalPrice);
        _userRepository.UpdateUser(user);

        //Create order
        var order = Order.Create(
            Address.Create(command.AddressCommand.Country, command.AddressCommand.City,
                command.AddressCommand.Street,
                command.AddressCommand.HouseNumber),
            totalPrice,
            user.Id);
        var items = cart.ShopCartItems.Select(item =>
            OrderItem.Create(item.Price, order.Id, item.CatId)).ToList();
        order.AddItems(items);
        await _orderRepository.CreateOrderAsync(order);

        await _cacheService.RemoveDataAsync(user.Id.ToString());
        return order;
    }
}