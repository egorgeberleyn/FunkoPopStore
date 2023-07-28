using KittyStore.Application.Common.Interfaces.Utils;

namespace KittyStore.Infrastructure.Utils
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}