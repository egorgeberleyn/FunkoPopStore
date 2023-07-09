using KittyStore.Domain.Common.Models;
using KittyStore.Domain.OrderAggregate.Entities;
using KittyStore.Domain.OrderAggregate.ValueObjects;

namespace KittyStore.Domain.OrderAggregate
{
    public sealed class Order : AggregateRoot
    {
        private readonly List<OrderItem> _items = new();
    
        public Address Address { get; private set; }
    
        public Guid UserId { get; private set;}
    
        public DateTime Created { get; private set;}

        public decimal TotalPrice { get; private set; }

        public IReadOnlyList<OrderItem> OrderItems => _items;

        private Order(Guid id, Address address, Guid userId, decimal totalPrice, DateTime created) : base(id)
        {
            Address = address;
            UserId = userId;
            Created = created;
            TotalPrice = totalPrice;
        }

        public static Order Create(Address address, decimal totalPrice, Guid userId) =>
            new(Guid.NewGuid(), address, userId, totalPrice, DateTime.UtcNow);

        public void AddItems(IEnumerable<OrderItem> items) => _items.AddRange(items);
        
        #pragma warning disable CS8618
            private Order() { }
        #pragma warning disable CS8618
    }
}