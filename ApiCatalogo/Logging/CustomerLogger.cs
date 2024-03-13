using System.Collections.Concurrent;

namespace ApiCatalogo.Logging;

public class CustomerLogger : ILogger
{
    private readonly CustomLoggerProviderConfiguration loggerConfig;
    readonly string loggerName;

    public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
    {
        loggerName = name;
        loggerConfig = config;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState,
        Exception, string> formatter)
    {
        string message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
        WriteTextInFile(message);
    }

    private void WriteTextInFile(string message)
    {
        string pathLogFile = @"/home/matheus/matheus-dev/code/cs/cs-studies/ApiCatalogo/ApiCatalogo/Data/log/ApiCatalogo_log.txt";

        using (StreamWriter streamWriter = new StreamWriter(pathLogFile, true))
        {
            try
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}