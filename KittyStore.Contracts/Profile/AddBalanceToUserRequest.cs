namespace KittyStore.Contracts.Profile
{
    public record AddBalanceToUserRequest(
        Guid UserId,
        decimal Amount);
}
