namespace HPIndigoSysValTool.Handlers.Logger;

public class LoggerFactory : ILoggerFactory
{
    public BaseLogger CreateLogger(LoggerType loggerType)
    {
        switch (loggerType)
        {
            case LoggerType.File:
                return FileLogger.Instance;
            case LoggerType.Html:
                return HtmlLogger.Instance;
            case LoggerType.Json:
                return JsonLogger.Instance;
            default:
                return null;
        }
    }
}