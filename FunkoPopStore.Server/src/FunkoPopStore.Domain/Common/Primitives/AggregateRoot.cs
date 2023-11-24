namespace FunkoPopStore.Domain.Common.Primitives;

public class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }

    protected AggregateRoot()
    {
    }
}