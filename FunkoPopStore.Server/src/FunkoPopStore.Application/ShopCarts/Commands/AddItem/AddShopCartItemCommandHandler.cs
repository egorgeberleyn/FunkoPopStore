using ErrorOr;
using FunkoPopStore.Application.Common.Interfaces.Authentication;
using FunkoPopStore.Application.Common.Interfaces.Cache;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.Common.Errors;
using FunkoPopStore.Domain.ShopCartAggregate;
using MediatR;

namespace FunkoPopStore.Application.ShopCarts.Commands.AddItem;

public class AddShopCartItemCommandHandler : IRequestHandler<AddShopCartItemCommand, ErrorOr<ShopCart>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ICacheService _cacheService;
    private readonly IFigureRepository _figureRepository;

    public AddShopCartItemCommandHandler(ICacheService cacheService,
        IFigureRepository figureRepository, ICurrentUserService currentUserService)
    {
        _cacheService = cacheService;
        _figureRepository = figureRepository;
        _currentUserService = currentUserService;
    }

    public async Task<ErrorOr<ShopCart>> Handle(AddShopCartItemCommand command, CancellationToken cancellationToken)
    {
        if (!_currentUserService.TryGetUserId(out var userId))
            return Errors.User.NotFound;

        var cart = await _cacheService.GetDataAsync<ShopCart>(userId.ToString())
                   ?? ShopCart.Create(userId);

        //Check that the cat is in the database but not in the cart
        if (await _figureRepository.GetFigureByIdAsync(command.CatId) is not { } cat)
            return Errors.Figure.NotFound;
        if (cart.ShopCartItems.FirstOrDefault(item => item.CatId == cat.Id) is not null)
            return Errors.Figure.AlreadyExist;

        cart.AddItem(cat.Price, cat.Id);

        await _cacheService.SetDataAsync(userId.ToString(), cart,
            DateTimeOffset.Now.AddDays(10));
        return cart;
    }
}