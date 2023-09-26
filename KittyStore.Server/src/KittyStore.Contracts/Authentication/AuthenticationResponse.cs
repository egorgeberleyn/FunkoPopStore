namespace KittyStore.Contracts.Authentication
{
    public record AuthenticationResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Role,
        string Token,
        string RefreshToken);
}