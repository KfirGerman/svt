using System.Text;
using Serilog.Events;
using Serilog.Formatting;

namespace HPIndigoSysValTool.Handlers.Logger;

public sealed class HtmlLogger : BaseLogger
{
    private static readonly string HtmlPath = "C:\\SVT_Logs\\HPIndigoSysValTool_.html";
    private static readonly ITextFormatter HtmlFormatter = new HtmlTextFormatter();

    private HtmlLogger() : base(HtmlFormatter, HtmlPath)
    {
    }

    public static HtmlLogger Instance { get; } = new();
}

public class HtmlTextFormatter : ITextFormatter
{
    public void Format(LogEvent logEvent, TextWriter output)
    {
        var messageBuilder = new StringBuilder();

        messageBuilder.Append("<div>");
        messageBuilder.Append($"<span style='color:gray;'>{logEvent.Timestamp:dd-MM-yyyy HH:mm:ss} | </span>");
        messageBuilder.Append($"<span style='color:{GetLogLevelColor(logEvent.Level)};'>{logEvent.Level:u3} | </span>");
        messageBuilder.Append($"{logEvent.RenderMessage()}</div>");

        if (logEvent.Exception != null) messageBuilder.Append($"<pre style='color:red;'>{logEvent.Exception}</pre>");

        output.WriteLine(messageBuilder.ToString());
    }

    private string GetLogLevelColor(LogEventLevel level)
    {
        return level switch
        {
            LogEventLevel.Verbose => "gray",
            LogEventLevel.Debug => "green",
            LogEventLevel.Information => "blue",
            LogEventLevel.Warning => "orange",
            LogEventLevel.Error => "red",
            LogEventLevel.Fatal => "purple",
            _ => "black"
        };
    }
}