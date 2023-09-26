using KittyStore.Domain.Common.Primitives;

namespace KittyStore.Domain.OrderAggregate.Events;

public record OrderPlaced(Order Order) : IDomainEvent;