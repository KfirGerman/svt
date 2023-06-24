namespace HPIndigoSysValTool.Validation
{
    /// <summary>
    /// Represents a diagnostic test for a hardware component.
    /// </summary>
    public interface IDiagnosticTest
    {
        /// <summary>
        /// Runs the diagnostic test and returns the result.
        /// </summary>
        /// <returns>A DiagnosticTestResult object containing the test result, including test ID, starting time, ending time, duration, status, and any other test-specific details.</returns>
        DiagnosticTestResult Run();

        /// <summary>
        /// Determines whether the diagnostic test requires the computer to be rebooted.
        /// </summary>
        /// <returns>True if a reboot is required, otherwise false.</returns>
        bool IsRebootRequired { get; }

        string TestDescription { get; }

    }
}