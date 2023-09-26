using KittyStore.Application.ShopCarts.Commands.AddItem;
using KittyStore.Contracts.ShopCart;
using KittyStore.Domain.ShopCartAggregate;
using KittyStore.Domain.ShopCartAggregate.Entities;
using Mapster;

namespace KittyStore.Api.Common.Mapping
{
    public class ShopCartMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ShopCart, ShopCartResponse>();

            config.NewConfig<ShopCartItem, ShopCartItemResponse>();

            config.NewConfig<AddShopCartItemRequest, AddShopCartItemCommand>();
        }
    }
}