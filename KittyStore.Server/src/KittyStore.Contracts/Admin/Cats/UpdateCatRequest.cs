namespace KittyStore.Contracts.Admin.Cats
{
    public record UpdateCatRequest(
        string Name,
        int Age,
        string Color,
        string Breed,
        decimal Price,
        string Gender);
}