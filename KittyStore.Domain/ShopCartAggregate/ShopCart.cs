using System.Text.Json.Serialization;
using KittyStore.Domain.CatAggregate.ValueObjects;
using KittyStore.Domain.Common.Models;
using KittyStore.Domain.ShopCartAggregate.Entities;
using KittyStore.Domain.ShopCartAggregate.ValueObjects;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Domain.ShopCartAggregate;

public sealed class ShopCart : AggregateRoot<ShopCartId>
{
    private readonly List<ShopCartItem> _items = new();
    
    public UserId UserId { get; private set; }

    public IReadOnlyList<ShopCartItem> ShopCartItems => _items;

    public int ItemQuantity
    {
        get => _items.Count;
        init { if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value)); }
    }

    public ShopCart(ShopCartId id, UserId userId) : base(id)
    {
        UserId = userId;
    }

    [JsonConstructor]
    public ShopCart(ShopCartId id, UserId userId, int itemQuantity, List<ShopCartItem> shopCartItems) : base(id)
    {
        UserId = userId;
        ItemQuantity = itemQuantity;
        _items = shopCartItems;
    }
    
    public static ShopCart Create(UserId userId) => 
        new (ShopCartId.CreateUnique(), userId);
    
    public void AddItem(decimal unitPrice, CatId catId) =>
        _items.Add(ShopCartItem.Create(unitPrice, catId, Id));
    
    public void RemoveItem(ShopCartItemId shopCartItemId)
    {
        var shopItem = _items.FirstOrDefault(item => item.Id == shopCartItemId);
        if (shopItem is not null) _items.Remove(shopItem);
    }
}