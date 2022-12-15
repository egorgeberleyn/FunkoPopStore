using KittyStore.Application.Common.Interfaces.Cache;
using KittyStore.Domain.ShopCartAggregate;
using MapsterMapper;
using MediatR;
using ErrorOr;
using KittyStore.Application.ShopCarts.Common;
using KittyStore.Domain.Common.Errors;

namespace KittyStore.Application.ShopCarts.Commands.RemoveItem;

public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand, ErrorOr<ShopCart>>
{
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;

    public RemoveItemCommandHandler(ICacheService cacheService, IMapper mapper)
    {
        _cacheService = cacheService;
        _mapper = mapper;
    }

    public async Task<ErrorOr<ShopCart>> Handle(RemoveItemCommand command, CancellationToken cancellationToken)
    {
        var cart = _mapper.Map<ShopCart>(
            await _cacheService.GetDataAsync<ShopCartDto>("shopCart"));

        if (cart is null)
            return Errors.ShopCart.ShopCartEmpty;
        
        if(cart.ShopCartItems.FirstOrDefault(item => item.Id == command.Id) is null)
            return Errors.ShopCart.NotFoundItem;
            
        cart.RemoveItem(command.Id);
        await _cacheService.SetDataAsync("shopCart", _mapper.Map<ShopCartDto>(cart),
            DateTimeOffset.Now.AddDays(10));
        
        return cart;
    }
}