using KittyStore.Domain.Common.Models;

namespace KittyStore.Domain.OrderAggregate.Events;

public record OrderIsProcessed(Order Order): IDomainEvent;