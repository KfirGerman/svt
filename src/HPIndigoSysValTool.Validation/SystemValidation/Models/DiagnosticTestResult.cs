namespace HPIndigoSysValTool.Validation;

public class DiagnosticTestResult : IDiagnosticTestResult
{
    /// <summary>
    /// Gets or sets the test ID.
    /// </summary>
    public string TestID { get; set; }

    /// <summary>
    /// Gets or sets the hardware component associated with the diagnostic test.
    /// </summary>
    public HardwareComponent HardwareComponent { get; set; }

    /// <summary>
    /// Gets or sets the starting time of the diagnostic test.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets the ending time of the diagnostic test.
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Gets or sets the duration of the diagnostic test.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Gets or sets the status of the diagnostic test (NotStarted, Running, ExecutionPassed, ExecutionFailed).
    /// </summary>
    public TestState State { get; set; }

    /// <summary>
    /// Gets or sets any other test-specific details as a dictionary, or a custom class.
    /// </summary>
    public IDictionary<string, object> Details { get; set; }

    /// <summary>
    /// Gets or sets the human-readable name for the diagnostic test.
    /// </summary>
    public string TestName { get; set; }

    /// <summary>
    /// Gets or sets the category of the diagnostic test, used to group similar tests together.
    /// </summary>
    public string TestCategory { get; set; }

    /// <summary>
    /// Gets or sets the error message or exception details that occurred during the test execution, if any.
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets a boolean value indicating whether the test passed or failed.
    /// </summary>
    public bool IsPassed { get; set; }

    /// <summary>
    /// Gets or sets the description of the test.
    /// </summary>
    public string TestDescription { get; set; }

}
