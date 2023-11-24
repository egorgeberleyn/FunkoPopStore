namespace FunkoPopStore.Domain.Common.Primitives;

public interface IHasDomainEvents
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    public void ClearDomainEvents();
}