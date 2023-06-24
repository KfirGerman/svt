namespace HPIndigoSysValTool.Validation.TestHistory;

/// <summary>
/// The interface for data logging
/// </summary>
public interface IHistory
{
    /// <summary>
    /// Returns the last execution result for a given test
    /// </summary>
    bool? GetLastResult(Guid id);

    /// <summary>
    /// Adds the results for the list of Tools
    /// </summary>
    void LogReading(IEnumerable<IDiagnosticTestResult> test);

    IEnumerable<HistoryRecord> Records { get; }

}