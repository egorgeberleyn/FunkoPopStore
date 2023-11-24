using FunkoPopStore.Domain.Common.Primitives;
using Newtonsoft.Json;

namespace FunkoPopStore.Domain.ShopCartAggregate.Entities;

public sealed class ShopCartItem : Entity
{
    public decimal Price { get; private set; }

    public Guid CatId { get; private set; }

    public Guid ShopCartId { get; private set; }

    [JsonConstructor]
    private ShopCartItem(Guid id, decimal price, Guid catId, Guid shopCartId) : base(id)
    {
        Price = price;
        CatId = catId;
        ShopCartId = shopCartId;
    }

    public static ShopCartItem Create(decimal price, Guid catId, Guid shopCartId) =>
        new(Guid.NewGuid(), price, catId, shopCartId);
}