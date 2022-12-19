using KittyStore.Application.Common.Interfaces.Cache;
using KittyStore.Domain.ShopCartAggregate;
using MapsterMapper;
using MediatR;
using ErrorOr;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.ShopCarts.Common;
using KittyStore.Domain.Common.Errors;

namespace KittyStore.Application.ShopCarts.Commands.RemoveItem
{
    public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand, ErrorOr<ShopCart>>
    {
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public RemoveItemCommandHandler(ICacheService cacheService, IMapper mapper, IUserRepository userRepository)
        {
            _cacheService = cacheService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<ShopCart>> Handle(RemoveItemCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetUserByIdAsync(command.UserId) is not { } user)
                return Errors.User.NotFound;
        
            var cart = _mapper.Map<ShopCart>(
                await _cacheService.GetDataAsync<ShopCartDto>(user.Id.ToString()));

            if (cart is null || cart.ShopCartItems.Count == 0)
                return Errors.ShopCart.ShopCartEmpty;
        
            if(cart.ShopCartItems.FirstOrDefault(item => item.Id == command.Id) is null)
                return Errors.ShopCart.NotFoundItem;
            
            cart.RemoveItem(command.Id);
            await _cacheService.SetDataAsync(user.Id.ToString(), _mapper.Map<ShopCartDto>(cart),
                DateTimeOffset.Now.AddDays(10));
        
            return cart;
        }
    }
}