namespace KittyStore.Contracts.Profile;

public record AddBalanceRequest(
    Guid UserId,
    decimal Amount);
