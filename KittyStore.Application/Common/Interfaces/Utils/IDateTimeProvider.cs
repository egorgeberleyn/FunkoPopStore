namespace KittyStore.Application.Common.Interfaces.Utils
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}