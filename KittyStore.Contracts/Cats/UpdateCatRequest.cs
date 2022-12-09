namespace KittyStore.Contracts.Cats;

public record UpdateCatRequest(
    Guid Id,
    string Name,
    int Age,
    string Color,
    string Breed,
    decimal Price);