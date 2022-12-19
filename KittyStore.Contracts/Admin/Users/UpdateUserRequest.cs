namespace KittyStore.Contracts.Users;

public record UpdateUserRequest(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    decimal Balance);
