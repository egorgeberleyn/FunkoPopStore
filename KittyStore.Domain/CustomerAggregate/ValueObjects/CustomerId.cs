using KittyStore.Domain.Common.Models;

namespace KittyStore.Domain.CustomerAggregate.ValueObjects;

public class CustomerId : ValueObject
{
    public Guid Value { get;}

    private CustomerId(Guid value)
    {
        Value = value;
    }

    public static CustomerId CreateUnique() => new(Guid.NewGuid());
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}