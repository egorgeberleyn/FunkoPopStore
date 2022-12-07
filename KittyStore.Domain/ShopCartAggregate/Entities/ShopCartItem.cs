using KittyStore.Domain.CatAggregate.ValueObjects;
using KittyStore.Domain.Common.Models;
using KittyStore.Domain.ShopCartAggregate.ValueObjects;

namespace KittyStore.Domain.ShopCartAggregate.Entities;

public sealed class ShopCartItem : Entity<ShopCartItemId>
{
    public decimal Price { get; }
    
    public CatId CatId { get; }
    
    public ShopCartId ShopCartId { get; }
    
    public ShopCartItem(ShopCartItemId id, decimal price, CatId catId, ShopCartId shopCartId) : base(id)
    {
        Price = price;
        CatId = catId;
        ShopCartId = shopCartId;
    }

    public static ShopCartItem Create(decimal price, CatId catId, ShopCartId shopCartId) =>
        new(ShopCartItemId.CreateUnique(), price, catId, shopCartId);
}