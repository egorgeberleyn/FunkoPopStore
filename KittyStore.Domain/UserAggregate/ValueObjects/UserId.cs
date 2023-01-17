using KittyStore.Domain.Common.Models;

namespace KittyStore.Domain.UserAggregate.ValueObjects
{
    public class UserId : ValueObject
    {
        public Guid Value { get;}

        private UserId(Guid value)
        {
            Value = value;
        }

        public static UserId CreateUnique() => new(Guid.NewGuid());
        
        public static UserId Create(Guid value) => new(value);
    
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}