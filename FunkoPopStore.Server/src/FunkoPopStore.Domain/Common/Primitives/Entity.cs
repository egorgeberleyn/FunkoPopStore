namespace FunkoPopStore.Domain.Common.Primitives;

public abstract class Entity : IEquatable<Entity>, IHasDomainEvents
{
    private readonly List<IDomainEvent> _domainEvents = new();


    public Guid Id { get; }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected Entity(Guid id)
    {
        Id = id;
    }

    public bool Equals(Entity? other) =>
        Equals((object?)other);

    public override bool Equals(object? obj) =>
        obj is Entity entity && Id.Equals(entity.Id);

    public override int GetHashCode() =>
        Id.GetHashCode();

    public static bool operator ==(Entity left, Entity right) =>
        Equals(left, right);

    public static bool operator !=(Entity left, Entity? right) =>
        !Equals(left, right);

    protected void AddDomainEvent(IDomainEvent @event) =>
        _domainEvents.Add(@event);

    public void ClearDomainEvents() =>
        _domainEvents.Clear();

#pragma warning disable CS8618
    protected Entity()
    {
    }
#pragma warning restore CS8618
}