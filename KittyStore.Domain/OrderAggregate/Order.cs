using KittyStore.Domain.Common.Models;
using KittyStore.Domain.OrderAggregate.Entities;
using KittyStore.Domain.OrderAggregate.ValueObjects;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Domain.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = new();
    
    public Address Address { get; private set; }
    
    public UserId UserId { get; private set;}
    
    public DateTime Created { get; private set;}

    public decimal TotalPrice { get; private set; }

    public IReadOnlyList<OrderItem> OrderItems => _items;

    private Order(OrderId id, Address address, UserId userId, DateTime created) : base(id)
    {
        Address = address;
        UserId = userId;
        Created = created;
    }

    public static Order Create(Address address, UserId userId) =>
        new(OrderId.CreateUnique(), address, userId, DateTime.UtcNow);

    public void AddItems(List<OrderItem> items) => _items.AddRange(items);

    public decimal CalculateTotalPrice()
    {
        TotalPrice = _items.Sum(ord => ord.Price);
        return TotalPrice;
    }
}