using KittyStore.Domain.Common.Models;

namespace KittyStore.Domain.ShopCartAggregate.ValueObjects
{
    public class ShopCartItemId : ValueObject
    {
        public Guid Value { get; }

        public ShopCartItemId(Guid value)
        {
            Value = value;
        }

        public static ShopCartItemId CreateUnique() => new(Guid.NewGuid());
    
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    
        public override string ToString() => Value.ToString();
    }
}