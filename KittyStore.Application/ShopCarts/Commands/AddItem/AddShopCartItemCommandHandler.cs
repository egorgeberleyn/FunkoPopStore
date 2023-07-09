using MediatR;
using ErrorOr;
using KittyStore.Application.Common.Interfaces.Cache;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.Common.Interfaces.Utils;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.ShopCartAggregate;

namespace KittyStore.Application.ShopCarts.Commands.AddItem
{
    public class AddShopCartItemCommandHandler : IRequestHandler<AddShopCartItemCommand, ErrorOr<ShopCart>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ICacheService _cacheService;
        private readonly ICatRepository _catRepository;

        public AddShopCartItemCommandHandler(ICacheService cacheService, 
            ICatRepository catRepository, ICurrentUserService currentUserService)
        {
            _cacheService = cacheService;
            _catRepository = catRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ErrorOr<ShopCart>> Handle(AddShopCartItemCommand command, CancellationToken cancellationToken)
        {
            if (!_currentUserService.TryGetUserId(out var userId))
                return Errors.User.NotFound;

            var cart = await _cacheService.GetDataAsync<ShopCart>(userId.ToString())
                ?? ShopCart.Create(userId);
            
            //Check that the cat is in the database but not in the cart
            if ( await _catRepository.GetCatByIdAsync(command.CatId) is not { } cat)
                return Errors.Cat.NotFound;
            if (cart.ShopCartItems.FirstOrDefault(item => item.CatId == cat.Id) is not null)
                return Errors.Cat.AlreadyExist;
       
            cart.AddItem(cat.Price, cat.Id);
        
            await _cacheService.SetDataAsync(userId.ToString(), cart,
                DateTimeOffset.Now.AddDays(10));
            return cart;
        }
    }
}