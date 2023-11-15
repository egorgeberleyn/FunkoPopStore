using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace KittyStore.Infrastructure.Persistence.Interceptors;

public class SlowQueryInterceptor : DbCommandInterceptor
{
    private readonly ILogger<SlowQueryInterceptor> _logger;

    public SlowQueryInterceptor(ILogger<SlowQueryInterceptor> logger)
    {
        _logger = logger;
    }

    private const int SlowQueryThreshold = 200; //milliseconds

    public override DbDataReader ReaderExecuted(
        DbCommand command, 
        CommandExecutedEventData eventData, 
        DbDataReader result)
    {
        if (eventData.Duration.TotalMilliseconds > SlowQueryThreshold)
        {
            //Log the slow query. Replace this with your actual logging mechanism.
            _logger.LogWarning("Slow query ({DurationTotalMilliseconds} ms): {CommandCommandText}", 
                eventData.Duration.TotalMilliseconds, command.CommandText);
        }
        
        return base.ReaderExecuted(command, eventData, result);
    }
}