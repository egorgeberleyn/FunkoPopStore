using FunkoPopStore.Domain.Common.Primitives;

namespace FunkoPopStore.Domain.SeriesAggregate;

public class Series : AggregateRoot
{
    public string Name { get; set; }
    
    private Series(Guid id, string name) : base(id)
    {
        Name = name;
    }
}