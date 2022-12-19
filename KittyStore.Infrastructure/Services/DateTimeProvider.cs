using KittyStore.Application.Common.Interfaces.Services;

namespace KittyStore.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}