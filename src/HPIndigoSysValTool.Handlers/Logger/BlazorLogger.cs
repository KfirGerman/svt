using Serilog.Formatting;
using Serilog.Formatting.Display;

namespace HPIndigoSysValTool.Handlers.Logger;

public sealed class BlazorLogger : BaseLogger
{
    private static readonly string LogDirectoryPath = "C:\\SVT_Logs";

    private static readonly ITextFormatter FileFormatter =
        new MessageTemplateTextFormatter(
            "{Timestamp:dd-MM-yyyy HH:mm:ss} | {Level:u3} | {Message:lj}{NewLine}{Exception}");

    private BlazorLogger() : base(FileFormatter, GetLogFilePath())
    {
    }

    public static BlazorLogger Instance { get; } = new();

    private static string GetLogFilePath()
    {
        if (!Directory.Exists(LogDirectoryPath)) Directory.CreateDirectory(LogDirectoryPath);

        var fileName = $"HPIndigoSysValTool_Blazor.log";
        return Path.Combine(LogDirectoryPath, fileName);
    }
}