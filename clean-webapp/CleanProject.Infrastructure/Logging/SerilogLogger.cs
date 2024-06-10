using CleanProject.CoreApplication.Infrastructure;
using Serilog;

namespace CleanProject.Infrastructure.Logging;

public class SerilogLogger<T> : IAppLogger<T>
{
    private readonly ILogger _logger;

    public SerilogLogger()
    {
        _logger = Log.ForContext<T>();
    }

    public void LogInformation(string message)
    {
        _logger.Information(message);
    }

    public void LogWarning(string message)
    {
        _logger.Warning(message);
    }

    public void LogError(string message, Exception ex)
    {
        _logger.Error(ex, message);
    }
}