using KittyStore.Domain.CatAggregate.ValueObjects;
using KittyStore.Domain.Common.Models;
using KittyStore.Domain.OrderAggregate.ValueObjects;

namespace KittyStore.Domain.OrderAggregate.Entities
{
    public sealed class OrderItem : Entity<OrderItemId>
    {
        public OrderId OrderId { get; private set; }
    
        public decimal Price { get; private set;}
    
        public CatId CatId { get; private set;}

        public Order? Order { get; set; }

        private OrderItem(OrderItemId id, OrderId orderId, decimal price, CatId catId) : base(id)
        {
            OrderId = orderId;
            Price = price;
            CatId = catId;
        }

        public static OrderItem Create(decimal price, OrderId orderId, CatId catId) =>
            new(OrderItemId.CreateUnique(), orderId, price, catId);
        
        #pragma warning disable CS8618
            private OrderItem() { }
        #pragma warning disable CS8618
    }
}