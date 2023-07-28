namespace KittyStore.Contracts.Orders
{
    public record OrderResponse(
        Guid Id,
        AddressResponse Address,
        Guid UserId,
        decimal TotalPrice,
        List<OrderItemResponse> OrderItems);
    
    public record OrderItemResponse(
        Guid Id,
        decimal Price,
        Guid CatId);

    public record AddressResponse(
        string Country, 
        string City,
        string Street,
        string HouseNumber);
}