namespace KittyStore.Application.ShopCarts.Common
{
    public record ShopCartDto(
        Guid Id,
        Guid UserId,
        List<ShopCartItemDto> ShopCartItems,
        int ItemQuantity,
        decimal TotalPrice);

    public record ShopCartItemDto(
        Guid Id,
        decimal Price,
        Guid CatId,
        Guid ShopCartId);
}
