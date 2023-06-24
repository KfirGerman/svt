using System.Collections.Generic;
using HPIndigoSysValTool.SystemInfo;

namespace HPIndigoSysValTool.Validation
{
    /// <summary>
    /// Represents a press machine with its series, model, and diagnostic test capabilities.
    /// </summary>
    public interface IPressMachine
    {
        /// <summary>
        /// Gets or sets the series of the press machine.
        /// </summary>
        string Series { get; set; }

        /// <summary>
        /// Gets or sets the model of the press machine.
        /// </summary>
        string Model { get; set; }

        /// <summary>
        /// Runs a specified diagnostic test on the specified hardware component and returns the combined results of the current and previous tests.
        /// </summary>
        /// <param name="testName">The name of the diagnostic test to run.</param>
        /// <param name="hardwareComponentName">The name of the hardware component to run the diagnostic test on.</param>
        /// <returns>A DiagnosticTestResult object representing the combined results of the current and previous tests.</returns>
        DiagnosticTestResult RunDiagnosticTest(string testName, string hardwareComponentName);

        /// <summary>
        /// Gets a dictionary containing the available diagnostic tests with their names as keys and the corresponding hardware component names as values.
        /// </summary>
        /// <returns>A dictionary of available diagnostic tests for the press machine with the test name as the key and the hardware component name as the value.</returns>
        Dictionary<string, List<string>> GetDiagnosticTests();
    }
}