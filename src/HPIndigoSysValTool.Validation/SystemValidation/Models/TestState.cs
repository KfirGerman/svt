namespace HPIndigoSysValTool.Validation;

public enum TestState
{
    NotStarted = 0,
    Running = 1,
    ExecutionPassed = 2,
    ExecutionFailed = 3,
    Cancelled = 4,
    Skipped = 5,
    Aborted = 6,
    Unknown = 7,
    TimedOut = 8
};