using KittyStore.Domain.Common.Models;

namespace KittyStore.Domain.AdminAggregate.ValueObjects;

public class AdminId : ValueObject
{
    public Guid Value { get;}

    private AdminId(Guid value)
    {
        Value = value;
    }

    public static AdminId CreateUnique() => new(Guid.NewGuid());
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}