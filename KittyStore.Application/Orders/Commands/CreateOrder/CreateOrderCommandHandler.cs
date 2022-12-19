using ErrorOr;
using KittyStore.Application.Common.Interfaces.Cache;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.ShopCarts.Common;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.OrderAggregate;
using KittyStore.Domain.OrderAggregate.Entities;
using KittyStore.Domain.OrderAggregate.ValueObjects;
using KittyStore.Domain.ShopCartAggregate;
using MapsterMapper;
using MediatR;

namespace KittyStore.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<Order>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, ICacheService cacheService, IMapper mapper, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _cacheService = cacheService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<Order>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetUserByIdAsync(command.UserId) is not { } user)
                return Errors.User.NotFound;
        
            //Get shopCart
            var cart = _mapper.Map<ShopCart>(
                await _cacheService.GetDataAsync<ShopCartDto>(user.Id.ToString()));
        
            //Validate and calculate
            if (cart is null || cart.ShopCartItems.Count == 0)
                return Errors.ShopCart.ShopCartEmpty;
            var totalPrice = cart.TotalPrice;
            if (totalPrice > user.Balance.Amount) return Errors.User.NotEnoughBalance;
            user.Balance.Debit(totalPrice);
        
            //Create order
            var order = Order.Create(
                Address.Create(command.AddressCommand.Country, command.AddressCommand.City, command.AddressCommand.Street, 
                    command.AddressCommand.HouseNumber),
                totalPrice,
                command.UserId);
            var items = cart.ShopCartItems.Select(item => 
                OrderItem.Create(item.Price, order.Id, item.CatId)).ToList();
            order.AddItems(items);
        
            //Save
            await _orderRepository.CreateOrderAsync(order);
            await _userRepository.UpdateUserAsync(user);
        
            await _cacheService.RemoveDataAsync(user.Id.ToString());
            return order;
        }
    }
}