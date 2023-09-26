using KittyStore.Domain.Common.Primitives;
using KittyStore.Domain.OrderAggregate.Entities;
using KittyStore.Domain.OrderAggregate.ValueObjects;
using KittyStore.Domain.OrderAggregate.Events;

namespace KittyStore.Domain.OrderAggregate
{
    public sealed class Order : AggregateRoot
    {
        private readonly List<OrderItem> _items = new();

        public Address Address { get; private set; }

        public string OrderNumber { get; private set; }

        public Guid UserId { get; private set; }

        public DateTime Created { get; private set; }

        public decimal TotalPrice { get; private set; }

        public IReadOnlyList<OrderItem> OrderItems => _items;

        private Order(Guid id, string orderNumber, Address address, Guid userId, decimal totalPrice,
            DateTime created) : base(id)
        {
            Address = address;
            UserId = userId;
            OrderNumber = orderNumber;
            Created = created;
            TotalPrice = totalPrice;
        }

        public static Order Create(Address address, decimal totalPrice, Guid userId)
        {
            var orderNumber = new Random()
                .Next(100000, 999999)
                .ToString();

            var order = new Order(Guid.NewGuid(), orderNumber, address, userId, totalPrice, DateTime.UtcNow);
            order.AddDomainEvent(new OrderPlaced(order));
            return order;
        }

        public void AddItems(IEnumerable<OrderItem> items) => _items.AddRange(items);

#pragma warning disable CS8618
        private Order()
        {
        }
#pragma warning disable CS8618
    }
}