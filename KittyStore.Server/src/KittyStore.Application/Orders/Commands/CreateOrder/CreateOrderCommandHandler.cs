using ErrorOr;
using KittyStore.Application.Common.Interfaces.Authentication;
using KittyStore.Application.Common.Interfaces.Cache;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.Common.Interfaces.Utils;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.OrderAggregate;
using KittyStore.Domain.OrderAggregate.Entities;
using KittyStore.Domain.OrderAggregate.ValueObjects;
using KittyStore.Domain.ShopCartAggregate;
using MediatR;

namespace KittyStore.Application.Orders.Commands.CreateOrder
{
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
            if(user is null)
                return Errors.User.NotFound;
            
            //Get shopCart
            var cart = await _cacheService.GetDataAsync<ShopCart>(user.Id.ToString());
        
            //Validate and calculate
            if (cart is null || cart.ShopCartItems.Count == 0)
                return Errors.ShopCart.ShopCartEmpty;
            var totalPrice = cart.TotalPrice;
            if (totalPrice > user.Balance?.Amount) return Errors.User.NotEnoughBalance;
            user.Balance?.Debit(totalPrice);
        
            //Create order
            var order = Order.Create(
                Address.Create(command.AddressCommand.Country, command.AddressCommand.City, command.AddressCommand.Street, 
                    command.AddressCommand.HouseNumber),
                totalPrice,
                user.Id);
            var items = cart.ShopCartItems.Select(item => 
                OrderItem.Create(item.Price, order.Id, item.CatId)).ToList();
            order.AddItems(items);
            
            await _orderRepository.CreateOrderAsync(order);
            _userRepository.UpdateUser(user);
        
            await _cacheService.RemoveDataAsync(user.Id.ToString());
            return order;
        }
    }
}