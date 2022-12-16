namespace KittyStore.Contracts.Orders;

public record CreateOrderRequest(
    Address Address,
    Guid UserId);

public record Address( 
    string Country, 
    string City,
    string Street,
    string HouseNumber);
