using KittyStore.Domain.Common.Models;
using KittyStore.Domain.OrderAggregate.Entities;
using KittyStore.Domain.OrderAggregate.ValueObjects;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Domain.OrderAggregate
{
    public sealed class Order : AggregateRoot<OrderId>
    {
        private readonly List<OrderItem> _items = new();
    
        public Address Address { get; private set; }
    
        public UserId UserId { get; private set;}
    
        public DateTime Created { get; private set;}

        public decimal TotalPrice { get; private set; }

        public IReadOnlyList<OrderItem> OrderItems => _items;

        public Order(OrderId id, Address address, UserId userId, decimal totalPrice, DateTime created) : base(id)
        {
            Address = address;
            UserId = userId;
            Created = created;
            TotalPrice = totalPrice;
        }

        public static Order Create(Address address, decimal totalPrice, UserId userId) =>
            new(OrderId.CreateUnique(), address, userId, totalPrice, DateTime.UtcNow);

        public void AddItems(List<OrderItem> items) => _items.AddRange(items);
    }
}