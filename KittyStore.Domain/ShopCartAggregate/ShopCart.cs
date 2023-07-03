using KittyStore.Domain.CatAggregate.ValueObjects;
using KittyStore.Domain.Common.Models;
using KittyStore.Domain.ShopCartAggregate.Entities;
using KittyStore.Domain.ShopCartAggregate.ValueObjects;
using KittyStore.Domain.UserAggregate.ValueObjects;
using Newtonsoft.Json;

namespace KittyStore.Domain.ShopCartAggregate
{
    public sealed class ShopCart : AggregateRoot<ShopCartId>
    {
        private readonly List<ShopCartItem> _items = new();
    
        public UserId UserId { get; private set; }

        public IReadOnlyList<ShopCartItem> ShopCartItems => _items;

        public int ItemsQuantity
        {
            get => _items.Count;
            init { if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value)); }
        }
    
        public decimal TotalPrice
        {
            get => CalculateTotalPrice();
            init { if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value)); }
        }

        public ShopCart(ShopCartId id, UserId userId) : base(id)
        {
            UserId = userId;
        }

        [JsonConstructor]
        public ShopCart(ShopCartId id, UserId userId, int itemsQuantity, List<ShopCartItem> shopCartItems,
            decimal totalPrice) : base(id)
        {
            UserId = userId;
            ItemsQuantity = itemsQuantity;
            _items = shopCartItems;
            TotalPrice = totalPrice;
        }
    
        public static ShopCart Create(UserId userId) => 
            new (ShopCartId.CreateUnique(), userId);
    
        public void AddItem(decimal price, CatId catId) =>
            _items.Add(ShopCartItem.Create(price, catId, Id));
    
        public void RemoveItem(ShopCartItemId shopCartItemId)
        {
            var shopItem = _items.FirstOrDefault(item => item.Id == shopCartItemId);
            if (shopItem is not null) _items.Remove(shopItem);
        }

        private decimal CalculateTotalPrice() => _items.Sum(cartItem => cartItem.Price);
   
    }
}