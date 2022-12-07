using KittyStore.Domain.Common.Models;

namespace KittyStore.Domain.OrderAggregate.ValueObjects;

public class OrderId : ValueObject
{
    public Guid Value { get;}

    private OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId CreateUnique() => new(Guid.NewGuid());
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}