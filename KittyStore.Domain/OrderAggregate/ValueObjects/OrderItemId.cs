using KittyStore.Domain.Common.Models;

namespace KittyStore.Domain.OrderAggregate.ValueObjects
{
    public class OrderItemId : ValueObject
    {
        public Guid Value { get;}

        private OrderItemId(Guid value)
        {
            Value = value;
        }

        public static OrderItemId CreateUnique() => new(Guid.NewGuid());
        
        public static OrderItemId Create(Guid value) => new(value);
    
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    
        public override string ToString() => Value.ToString();
    }
}