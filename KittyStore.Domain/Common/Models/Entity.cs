namespace KittyStore.Domain.Common.Models
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Guid Id { get;}

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
        
        #pragma warning disable CS8618
            protected Entity() { }
        #pragma warning restore CS8618
    }
}