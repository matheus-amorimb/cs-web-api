namespace ApiCatalogo.Logging;

public class CustomLoggerProviderConfiguration
{
    public LogLevel LogLevel { get; set; } = Microsoft.Extensions.Logging.LogLevel.Warning;

    public int EventId { get; set; } = 0;
}