using KittyStore.Domain.Common.Models;

namespace KittyStore.Domain.CatAggregate.ValueObjects
{
    public class CatId : ValueObject
    {
        public Guid Value { get; }

        private CatId(Guid value)
        {
            Value = value;
        }

        public static CatId CreateUnique() => new(Guid.NewGuid());
        
        public static CatId Create(Guid value) => new(value);
    
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    
        public override string ToString() => Value.ToString();
    }
}