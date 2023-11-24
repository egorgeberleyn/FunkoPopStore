using FunkoPopStore.Domain.Common.Primitives;
using FunkoPopStore.Domain.ShopCartAggregate.Entities;
using Newtonsoft.Json;

namespace FunkoPopStore.Domain.ShopCartAggregate;

public sealed class ShopCart : AggregateRoot
{
    private readonly List<ShopCartItem> _items = new();

    public Guid UserId { get; private set; }

    public IReadOnlyList<ShopCartItem> ShopCartItems => _items;

    public int ItemsQuantity
    {
        get => _items.Count;
        private set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
        }
    }

    public decimal TotalPrice
    {
        get => CalculateTotalPrice();
        private set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
        }
    }

    [JsonConstructor]
    private ShopCart(Guid id, Guid userId, int itemsQuantity, List<ShopCartItem> shopCartItems,
        decimal totalPrice) : base(id)
    {
        UserId = userId;
        ItemsQuantity = itemsQuantity;
        _items = shopCartItems;
        TotalPrice = totalPrice;
    }

    private ShopCart(Guid id, Guid userId) : base(id)
    {
        UserId = userId;
    }

    public static ShopCart Create(Guid userId) =>
        new(Guid.NewGuid(), userId);

    public void AddItem(decimal price, Guid catId) =>
        _items.Add(ShopCartItem.Create(price, catId, Id));

    public void RemoveItem(Guid shopCartItemId)
    {
        var shopItem = _items.FirstOrDefault(item => item.Id == shopCartItemId);
        if (shopItem is not null) _items.Remove(shopItem);
    }

    private decimal CalculateTotalPrice() => _items.Sum(cartItem => cartItem.Price);
}