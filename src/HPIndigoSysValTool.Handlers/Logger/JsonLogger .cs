using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace HPIndigoSysValTool.Handlers.Logger;

public sealed class JsonLogger : BaseLogger
{
    private static readonly string ExecutablePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    private static readonly ITextFormatter JsonFormatter = new CustomJsonFormatter();

    private JsonLogger() : base(JsonFormatter, GetLogFilePath())
    {
    }

    public static JsonLogger Instance { get; } = new();

    private static string GetLogFilePath()
    {
        var fileName = $"HPIndigoSysValTool_TestHistory.json";
        // path is relative to execution folder
        return Path.Combine(ExecutablePath, fileName);
    }
}

public class CustomJsonFormatter : ITextFormatter
{
    private readonly JsonFormatter _jsonFormatter;

    public CustomJsonFormatter(bool renderMessage = true, bool closingDelimiter = false)
    {
        _jsonFormatter = new JsonFormatter(null, renderMessage);
    }

    public void Format(LogEvent logEvent, TextWriter output)
    {
        if (logEvent == null)
        {
            throw new ArgumentNullException(nameof(logEvent));
        }
        if (output == null)
        {
            throw new ArgumentNullException(nameof(output));
        }

        // Filter out the log level property
        var properties = new Dictionary<string, LogEventPropertyValue>(logEvent.Properties);
        properties.Remove("@l");

        var customProperties = new List<LogEventProperty>();
        foreach (var property in properties)
        {
            customProperties.Add(new LogEventProperty(property.Key, property.Value));
        }

        var customLogEvent = new LogEvent(
            logEvent.Timestamp,
            logEvent.Level,
            logEvent.Exception,
            logEvent.MessageTemplate,
            customProperties);

        _jsonFormatter.Format(customLogEvent, output);
    }

}