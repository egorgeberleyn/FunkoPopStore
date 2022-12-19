namespace KittyStore.Contracts.ShopCart
{
    public record AddShopCartItemRequest(
        Guid UserId,
        decimal Price,
        Guid CatId);
}
