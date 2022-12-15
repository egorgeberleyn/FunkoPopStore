using KittyStore.Domain.CatAggregate.ValueObjects;
using KittyStore.Domain.Common.Models;
using KittyStore.Domain.OrderAggregate.ValueObjects;

namespace KittyStore.Domain.OrderAggregate.Entities;

public sealed class OrderItem : Entity<OrderItemId>
{
    public decimal Price { get; private set;}
    
    public CatId CatId { get; private set;}
    
    public OrderItem(OrderItemId id, decimal price, CatId catId) : base(id)
    {
        Price = price;
        CatId = catId;
    }

    public static OrderItem Create(OrderItemId id, decimal price, CatId catId) =>
        new(OrderItemId.CreateUnique(), price, catId);
}