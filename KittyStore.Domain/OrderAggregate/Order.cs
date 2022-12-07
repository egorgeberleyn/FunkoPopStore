using KittyStore.Domain.Common.Models;
using KittyStore.Domain.CustomerAggregate.ValueObjects;
using KittyStore.Domain.OrderAggregate.Entities;
using KittyStore.Domain.OrderAggregate.ValueObjects;

namespace KittyStore.Domain.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = new();
    
    public Address Address { get; }
    
    public CustomerId CustomerId { get; }
    
    public DateTime Created { get; }

    public decimal TotalPrice => CalculateTotalPrice();

    public IReadOnlyList<OrderItem> OrderItems => _items.AsReadOnly();

    private Order(OrderId id, Address address, CustomerId customerId, DateTime created) : base(id)
    {
        Address = address;
        CustomerId = customerId;
        Created = created;
    }

    public static Order Create(OrderId id, Address address, CustomerId customerId, DateTime created) =>
        new(OrderId.CreateUnique(), address, customerId, DateTime.UtcNow);

    public decimal CalculateTotalPrice() => _items.Sum(ord => ord.Price);
}