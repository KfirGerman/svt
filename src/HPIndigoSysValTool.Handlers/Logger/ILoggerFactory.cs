namespace HPIndigoSysValTool.Handlers.Logger;

public interface ILoggerFactory
{
    BaseLogger CreateLogger(LoggerType loggerType);
}

public enum LoggerType
{
    File,
    Html,
    Json
}