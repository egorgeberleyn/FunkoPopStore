using FunkoPopStore.Application.ShopCarts.Commands.AddItem;
using FunkoPopStore.Contracts.ShopCart;
using FunkoPopStore.Domain.ShopCartAggregate;
using FunkoPopStore.Domain.ShopCartAggregate.Entities;
using Mapster;

namespace FunkoPopStore.Api.Common.Mapping;

public class ShopCartMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ShopCart, ShopCartResponse>();

        config.NewConfig<ShopCartItem, ShopCartItemResponse>();

        config.NewConfig<AddShopCartItemRequest, AddShopCartItemCommand>();
    }
}