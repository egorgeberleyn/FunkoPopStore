namespace FunkoPopStore.Domain.OrderAggregate.Enums;

public enum OrderStatus
{
    None = 0,
    Created = 1,
    Shipped = 2,
    Completed = 3,
    Cancelled = 4
}