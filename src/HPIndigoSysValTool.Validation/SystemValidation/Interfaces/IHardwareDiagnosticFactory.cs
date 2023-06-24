using System.Collections.Generic;

namespace HPIndigoSysValTool.Validation
{
    public interface IHardwareDiagnosticFactory
    {
        /// <summary>
        /// Creates a single diagnostic test instance based on the provided test name and adds it to the dictionary of available tests.
        /// </summary>
        /// <param name="testName">The name of the diagnostic test to create.</param>
        void AddDiagnosticTest(string testName);

        /// <summary>
        /// Creates all available diagnostic tests for a specific hardware component and adds them to the dictionary of available tests.
        /// </summary>
        void AddAllDiagnosticTests();

        /// <summary>
        /// Gets a dictionary containing the available diagnostic tests with their names as keys and their corresponding IDiagnosticTest objects as values.
        /// </summary>
        /// <returns>A dictionary of available diagnostic tests for the hardware component.</returns>
        Dictionary<string, IDiagnosticTest> GetDiagnosticTests();

        /// <summary>
        /// Gets or sets a dictionary containing the available diagnostic tests with their names as keys and their corresponding IDiagnosticTest objects as values.
        /// </summary>
        Dictionary<string, IDiagnosticTest> DiagnosticTests { get; }
    }
}