using FunkoPopStore.Application.Common.Interfaces.Utils;

namespace FunkoPopStore.Infrastructure.Utils;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}