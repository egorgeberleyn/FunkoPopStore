using KittyStore.Domain.Common.Models;

namespace KittyStore.Domain.ShopCartAggregate.ValueObjects;

public class ShopCartId : ValueObject
{
    public Guid Value { get;}

    private ShopCartId(Guid value)
    {
        Value = value;
    }

    public static ShopCartId CreateUnique() => new(Guid.NewGuid());
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}