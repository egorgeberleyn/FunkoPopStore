using FunkoPopStore.Domain.Common.Primitives;

namespace FunkoPopStore.Domain.OrderAggregate.Events;

public record OrderPlaced(Order Order) : IDomainEvent;