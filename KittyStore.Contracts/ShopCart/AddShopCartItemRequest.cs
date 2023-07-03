namespace KittyStore.Contracts.ShopCart
{
    public record AddShopCartItemRequest(
        decimal Price,
        Guid CatId);
}
