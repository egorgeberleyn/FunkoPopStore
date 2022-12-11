namespace KittyStore.Contracts.Cats;

public record CreateCatRequest(
    string Name,
    int Age,
    string Color,
    string Breed,
    decimal Price,
    string Gender);