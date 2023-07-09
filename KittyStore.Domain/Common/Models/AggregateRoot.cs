namespace KittyStore.Domain.Common.Models
{
    public class AggregateRoot : Entity
    {
        protected AggregateRoot(Guid id) : base(id) {}

        protected AggregateRoot() { }
    }
}