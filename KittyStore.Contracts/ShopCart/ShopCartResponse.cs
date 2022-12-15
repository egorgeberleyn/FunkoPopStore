namespace KittyStore.Contracts.ShopCart;

public record ShopCartResponse(
    Guid Id,
    Guid UserId,
    List<ShopCartItemResponse> ShopCartItems,
    int ItemQuantity);

public record ShopCartItemResponse(
    Guid Id,
    decimal Price,
    Guid CatId,
    Guid ShopCartId);