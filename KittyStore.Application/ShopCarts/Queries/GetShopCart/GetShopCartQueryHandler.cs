using ErrorOr;
using KittyStore.Application.Common.Interfaces.Cache;
using KittyStore.Application.Common.Interfaces.Utils;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.ShopCartAggregate;
using MediatR;

namespace KittyStore.Application.ShopCarts.Queries.GetShopCart
{
    public class GetShopCartQueryHandler : IRequestHandler<GetShopCartQuery, ErrorOr<ShopCart>>
    {
        private readonly ICacheService _cacheService;
        private readonly ICurrentUserService _currentUserService;

        public GetShopCartQueryHandler(ICacheService cacheService,
            ICurrentUserService currentUserService)
        {
            _cacheService = cacheService;
            _currentUserService = currentUserService;
        }

        public async Task<ErrorOr<ShopCart>> Handle(GetShopCartQuery query, CancellationToken cancellationToken)
        {
            if (!_currentUserService.TryGetUserId(out var userId))
                return Errors.User.NotFound;

            var cart = await _cacheService.GetDataAsync<ShopCart>(userId.ToString())
                ?? ShopCart.Create(userId);
            
            await _cacheService.SetDataAsync(userId.ToString(), cart,
                DateTimeOffset.Now.AddDays(10));
            return cart;
        }
    }
}