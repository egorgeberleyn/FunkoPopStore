namespace KittyStore.Contracts.Cats;

public record CatResponse(
    Guid Id,
    string Name,
    int Age,
    string Color,
    string Breed,
    decimal Price,
    string Gender);