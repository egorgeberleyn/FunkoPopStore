namespace KittyStore.Contracts.Authentication;

public record TokenRequest(
    string Token,
    string RefreshToken);