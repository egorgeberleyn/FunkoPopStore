using KittyStore.Domain.Common.Models;

namespace KittyStore.Domain.OrderAggregate.ValueObjects;

public class OrderItemId : ValueObject
{
    public Guid Value { get; }

    public OrderItemId(Guid value)
    {
        Value = value;
    }

    public static OrderItemId CreateUnique() => new(Guid.NewGuid());
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}