using KittyStore.Domain.Common.Models;
using KittyStore.Domain.OrderAggregate.Entities;
using KittyStore.Domain.OrderAggregate.ValueObjects;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Domain.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = new();
    
    public Address Address { get; private set;}
    
    public UserId UserId { get; private set;}
    
    public DateTime Created { get; private set;}

    public decimal TotalPrice => CalculateTotalPrice();

    public IReadOnlyList<OrderItem> OrderItems => _items.AsReadOnly();

    private Order(OrderId id, Address address, UserId customerId, DateTime created) : base(id)
    {
        Address = address;
        UserId = customerId;
        Created = created;
    }

    public static Order Create(OrderId id, Address address, UserId customerId, DateTime created) =>
        new(OrderId.CreateUnique(), address, customerId, DateTime.UtcNow);

    public decimal CalculateTotalPrice() => _items.Sum(ord => ord.Price);
}