namespace KittyStore.Contracts.ShopCart
{
    public record ShopCartResponse(
        Guid Id,
        Guid UserId,
        List<ShopCartItemResponse> ShopCartItems,
        int ItemsQuantity,
        decimal TotalPrice);

    public record ShopCartItemResponse(
        Guid Id,
        decimal Price,
        Guid CatId,
        Guid ShopCartId);
}