using KittyStore.Application.ShopCarts.Commands.AddItem;
using KittyStore.Application.ShopCarts.Common;
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
            config.NewConfig<ShopCart, ShopCartResponse>()
                .Map(dist => dist.UserId, src => src.UserId.Value)
                .Map(dist => dist.Id, src => src.Id.Value);
        
            config.NewConfig<ShopCartItem, ShopCartItemResponse>()
                .Map(dist => dist.Id, src => src.Id.Value)
                .Map(dist => dist.CatId, src => src.CatId.Value)
                .Map(dist => dist.ShopCartId, src => src.ShopCartId.Value);

            config.NewConfig<AddShopCartItemRequest, AddShopCartItemCommand>()
                .Map(dist => dist.CatId.Value, src => src.CatId);
            
            //dto <-> domain root
            config.NewConfig<ShopCart, ShopCartDto>()
                .Map(dist => dist.UserId, src => src.UserId.Value)
                .Map(dist => dist.Id, src => src.Id.Value);
        
            config.NewConfig<ShopCartItem, ShopCartItemDto>()
                .Map(dist => dist.Id, src => src.Id.Value)
                .Map(dist => dist.CatId, src => src.CatId.Value)
                .Map(dist => dist.ShopCartId, src => src.ShopCartId.Value);
        
            config.NewConfig<ShopCartDto, ShopCart>().MapToConstructor(true)
                .Map(dist => dist.UserId.Value, src => src.UserId)
                .Map(dist => dist.Id.Value, src => src.Id);
        
            config.NewConfig<ShopCartItemDto, ShopCartItem>().MapToConstructor(true)
                .Map(dist => dist.Id.Value, src => src.Id)
                .Map(dist => dist.CatId.Value, src => src.CatId)
                .Map(dist => dist.ShopCartId.Value, src => src.ShopCartId);
        }
    }
}