using KittyStore.Domain.Common.Models;

namespace KittyStore.Domain.ShopCartAggregate.ValueObjects
{
    public class ShopCartItemId : ValueObject
    {
        public Guid Value {get;}

        private ShopCartItemId(Guid value)
        {
            Value = value;
        }

        public static ShopCartItemId CreateUnique() => new(Guid.NewGuid());
        
        public static ShopCartItemId Create(Guid value) => new(value);
    
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    
        public override string ToString() => Value.ToString();
    }
}