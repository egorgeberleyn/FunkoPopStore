﻿using KittyStore.Application.Common.Interfaces.Cache;
using KittyStore.Domain.ShopCartAggregate;
using MapsterMapper;
using MediatR;
using ErrorOr;
using KittyStore.Application.Common.Interfaces.Authentication;
using KittyStore.Domain.Common.Errors;

namespace KittyStore.Application.ShopCarts.Commands.RemoveItem
{
    public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand, ErrorOr<ShopCart>>
    {
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public RemoveItemCommandHandler(ICacheService cacheService, IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _cacheService = cacheService;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<ErrorOr<ShopCart>> Handle(RemoveItemCommand command, CancellationToken cancellationToken)
        {
            if (!_currentUserService.TryGetUserId(out var userId))
                return Errors.User.NotFound;

            //Get cart cache from redis
            var cart = await _cacheService.GetDataAsync<ShopCart>(userId.ToString());
            if (cart is null || cart.ShopCartItems.Count == 0)
                return Errors.ShopCart.ShopCartEmpty;

            if (cart.ShopCartItems.FirstOrDefault(item => item.Id == command.Id) is null)
                return Errors.ShopCart.NotFoundItem;

            cart.RemoveItem(command.Id);
            await _cacheService.SetDataAsync(userId.ToString(), cart,
                DateTimeOffset.Now.AddDays(10));

            return cart;
        }
    }
}