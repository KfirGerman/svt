namespace HPIndigoSysValTool.Handlers.Logger;

public static class LoggerProvider
{
    public static FileLogger FileLogger { get; } = FileLogger.Instance;
    public static HtmlLogger HtmlLogger { get; } = HtmlLogger.Instance;
    public static JsonLogger JsonLogger { get; } = JsonLogger.Instance;
}