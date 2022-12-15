using MediatR;
using ErrorOr;
using KittyStore.Application.Common.Interfaces.Cache;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.ShopCarts.Common;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.ShopCartAggregate;
using MapsterMapper;

namespace KittyStore.Application.ShopCarts.Commands.AddItem;

public class AddShopCartItemCommandHandler : IRequestHandler<AddShopCartItemCommand, ErrorOr<ShopCart>>
{
    private readonly ICacheService _cacheService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AddShopCartItemCommandHandler(ICacheService cacheService, IMapper mapper, IUserRepository userRepository)
    {
        _cacheService = cacheService;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<ShopCart>> Handle(AddShopCartItemCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByIdAsync(command.UserId) is not { } user)
            return Errors.User.NotFound;
        
        var cart = _mapper.Map<ShopCart>(
                       await _cacheService.GetDataAsync<ShopCartDto>("shopCart"))
                   ?? ShopCart.Create(user.Id);
        
        cart.AddItem(command.Price, command.CatId);
        
        await _cacheService.SetDataAsync("shopCart", _mapper.Map<ShopCartDto>(cart),
            DateTimeOffset.Now.AddDays(10));
        return cart;
    }
}