namespace FunkoPopStore.Contracts.Orders;

public record CreateOrderRequest(
    Address Address);

public record Address(
    string Country,
    string City,
    string Street,
    string HouseNumber);