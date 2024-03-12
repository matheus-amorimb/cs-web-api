using System.Collections.Concurrent;

namespace ApiCatalogo.Logging;

public class CustomerLogger : ILogger
{
    readonly string loggerName;

    private readonly CustomLoggerProviderConfiguration loggerConfig;

    public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
    {
        loggerName = name;
        loggerConfig = config;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }

    // public IDisposable BeginScope<TState>(TState state)
    // {
    //     return null;
    // }
    
}