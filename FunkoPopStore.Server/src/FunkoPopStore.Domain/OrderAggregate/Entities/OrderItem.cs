using FunkoPopStore.Domain.Common.Primitives;

namespace FunkoPopStore.Domain.OrderAggregate.Entities;

public sealed class OrderItem : Entity
{
    public Guid OrderId { get; private set; }

    public decimal Price { get; private set; }

    public Guid CatId { get; private set; }

    public Order? Order { get; set; }

    private OrderItem(Guid id, Guid orderId, decimal price, Guid catId) : base(id)
    {
        OrderId = orderId;
        Price = price;
        CatId = catId;
    }

    public static OrderItem Create(decimal price, Guid orderId, Guid catId) =>
        new(Guid.NewGuid(), orderId, price, catId);

#pragma warning disable CS8618
    private OrderItem()
    {
    }
#pragma warning disable CS8618
}