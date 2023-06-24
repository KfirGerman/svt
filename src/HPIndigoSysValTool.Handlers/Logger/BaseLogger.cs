using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace HPIndigoSysValTool.Handlers.Logger;

public abstract class BaseLogger : ILogger
{
    private static readonly Lazy<LoggerFactory> LoggerFactoryInstance = new(() => new LoggerFactory());
    private static readonly object LockObj = new();

    private readonly ILogger _logger;

    protected BaseLogger(ITextFormatter formatter, string path)
    {
        _logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.File(formatter, path)
            .CreateLogger();
    }


    public ILogger ForContext(ILogEventEnricher enricher)
    {
        return _logger.ForContext(enricher);
    }

    public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
    {
        return _logger.ForContext(enrichers);
    }

    public ILogger ForContext(string propertyName, object value, bool destructureObjects = false)
    {
        return _logger.ForContext(propertyName, value, destructureObjects);
    }

    public ILogger ForContext<TSource>()
    {
        return _logger.ForContext<TSource>();
    }

    public ILogger ForContext(Type source)
    {
        return _logger.ForContext(source);
    }


    public void Write(LogEvent logEvent)
    {
        _logger.Write(logEvent);
    }

    public void Write(LogEventLevel level, string messageTemplate)
    {
        _logger.Write(level, messageTemplate);
    }

    public void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
    {
        _logger.Write(level, messageTemplate, propertyValue);
    }

    public void Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        _logger.Write(level, messageTemplate, propertyValue0, propertyValue1);
    }

    public void Write(LogEventLevel level, Exception exception, string messageTemplate)
    {
        _logger.Write(level, exception, messageTemplate);
    }

    public void Write<T>(LogEventLevel level, Exception exception, string messageTemplate, T propertyValue)
    {
        _logger.Write(level, exception, messageTemplate, propertyValue);
    }

    public void Write<T0, T1>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0,
        T1 propertyValue1)
    {
        _logger.Write(level, exception, messageTemplate, propertyValue0, propertyValue1);
    }

    public bool IsEnabled(LogEventLevel level)
    {
        return _logger.IsEnabled(level);
    }

    public void Information(string messageTemplate)
    {
        _logger.Information(messageTemplate);
    }

    public void Information<T>(string messageTemplate, T propertyValue)
    {
        _logger.Information(messageTemplate, propertyValue);
    }

    public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        _logger.Information(messageTemplate, propertyValue0, propertyValue1);
    }

    public void Information(string messageTemplate, params object[] propertyValues)
    {
        _logger.Information(messageTemplate, propertyValues);
    }


    public void Verbose(string messageTemplate)
    {
        _logger.Verbose(messageTemplate);
    }

    public void Verbose<T>(string messageTemplate, T propertyValue)
    {
        _logger.Verbose(messageTemplate, propertyValue);
    }

    public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        _logger.Verbose(messageTemplate, propertyValue0, propertyValue1);
    }

    public void Verbose(Exception exception, string messageTemplate)
    {
        _logger.Verbose(exception, messageTemplate);
    }

    public void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        _logger.Verbose(exception, messageTemplate, propertyValue);
    }

    public void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        _logger.Verbose(exception, messageTemplate, propertyValue0, propertyValue1);
    }


    public void Error(string messageTemplate)
    {
        _logger.Error(messageTemplate);
    }

    public void Error<T>(string messageTemplate, T propertyValue)
    {
        _logger.Error(messageTemplate, propertyValue);
    }

    public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        _logger.Error(messageTemplate, propertyValue0, propertyValue1);
    }

    public void Error(Exception exception, string messageTemplate)
    {
        _logger.Error(exception, messageTemplate);
    }

    public void Error<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        _logger.Error(exception, messageTemplate, propertyValue);
    }

    public void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        _logger.Error(exception, messageTemplate, propertyValue0, propertyValue1);
    }


    public void Fatal(string messageTemplate)
    {
        _logger.Fatal(messageTemplate);
    }

    public void Fatal<T>(string messageTemplate, T propertyValue)
    {
        _logger.Fatal(messageTemplate, propertyValue);
    }

    public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        _logger.Fatal(messageTemplate, propertyValue0, propertyValue1);
    }

    public void Fatal(Exception exception, string messageTemplate)
    {
        _logger.Fatal(exception, messageTemplate);
    }

    public void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        _logger.Fatal(exception, messageTemplate, propertyValue);
    }

    public void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        _logger.Fatal(exception, messageTemplate, propertyValue0, propertyValue1);
    }

    public static BaseLogger GetLoggerInstance(LoggerType loggerType)
    {
        lock (LockObj)
        {
            return LoggerFactoryInstance.Value.CreateLogger(loggerType);
        }
    }

    public void Information(JObject jObject)
    {
        _logger.Information(jObject.ToString(Formatting.None));
    }

    public void Verbose(JObject jObject)
    {
        _logger.Verbose(jObject.ToString(Formatting.None));
    }

    public void Error(JObject jObject)
    {
        _logger.Error(jObject.ToString(Formatting.None));
    }

    public void Fatal(JObject jObject)
    {
        _logger.Fatal(jObject.ToString(Formatting.None));
    }
}