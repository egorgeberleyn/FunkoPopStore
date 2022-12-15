using ErrorOr;
using KittyStore.Application.Common.Interfaces.Cache;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.ShopCarts.Common;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.ShopCartAggregate;
using MapsterMapper;
using MediatR;

namespace KittyStore.Application.ShopCarts.Queries.GetShopCart;

public class GetShopCartQueryHandler : IRequestHandler<GetShopCartQuery, ErrorOr<ShopCart>>
{
    private readonly ICacheService _cacheService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetShopCartQueryHandler(ICacheService cacheService, IUserRepository userRepository, IMapper mapper)
    {
        _cacheService = cacheService;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<ShopCart>> Handle(GetShopCartQuery query, CancellationToken cancellationToken)
    {
        var cart = await _cacheService.GetDataAsync<ShopCartDto>("shopCart");
        if (cart is not null) return _mapper.Map<ShopCart>(cart);

        if (await _userRepository.GetUserByIdAsync(query.UserId) is not { } user)
            return Errors.User.NotFound;
        
        var newCart = ShopCart.Create(user.Id);
        await _cacheService.SetDataAsync("shopCart", _mapper.Map<ShopCartDto>(newCart), 
            DateTimeOffset.Now.AddDays(10));
            
        return newCart;
    }
}