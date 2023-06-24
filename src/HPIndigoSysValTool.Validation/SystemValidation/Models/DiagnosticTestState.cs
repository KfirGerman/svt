using Newtonsoft.Json.Linq;

namespace HPIndigoSysValTool.Validation;

/// <summary>
/// Represents the current state of a diagnostic test, including test name, hardware component, status, and diagnostic test result.
/// </summary>
public class DiagnosticTestState
{
    /// <summary>
    /// Gets or sets the name of the diagnostic test.
    /// </summary>
    public string TestName { get; set; }

    /// <summary>
    /// Gets or sets the hardware component associated with the diagnostic test.
    /// </summary>
    public HardwareComponent HardwareComponent { get; set; }

    /// <summary>
    /// Gets or sets the current status of the diagnostic test (NotStarted, Running, ExecutionPassed, ExecutionFailed).
    /// </summary>
    public TestState State { get; set; }

    /// <summary>
    /// Gets or sets the diagnostic test result.
    /// </summary>
    public DiagnosticTestResult DiagnosticTestResult { get; set; }

    /// <summary>
    /// Creates a new instance of the DiagnosticTestState class with the specified test name and diagnostic test result.
    /// </summary>
    public DiagnosticTestState(string testName, DiagnosticTestResult result)
    {
        TestName = testName;
        DiagnosticTestResult = result;
        HardwareComponent = result.HardwareComponent;
        State = result.State;
    }



}
