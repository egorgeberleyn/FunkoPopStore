using KittyStore.Domain.Common.Models;

namespace KittyStore.Domain.CatAggregate.ValueObjects
{
    public class CatId : ValueObject
    {
        public Guid Value { get; }

        public CatId(Guid value)
        {
            Value = value;
        }

        public static CatId CreateUnique() => new(Guid.NewGuid());
    
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    
        public override string ToString() => Value.ToString();
    }
}