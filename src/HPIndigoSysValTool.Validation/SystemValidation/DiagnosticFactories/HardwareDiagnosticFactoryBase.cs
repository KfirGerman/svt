using System.Collections.Generic;
using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.SystemInfo;

namespace HPIndigoSysValTool.Validation
{
    public abstract class HardwareDiagnosticFactoryBase : IHardwareDiagnosticFactory
    {
        protected readonly SysInfo _systemInfo;

        protected HardwareDiagnosticFactoryBase(SysInfo systemInfo)
        {
            _systemInfo = systemInfo;
            DiagnosticTests = new Dictionary<string, IDiagnosticTest>();
        }
        /// <summary>
        /// Gets or sets a dictionary containing the available diagnostic tests with their names as keys and their corresponding IDiagnosticTest objects as values.
        /// </summary>
        public Dictionary<string, IDiagnosticTest> DiagnosticTests { get; }
        /// <summary>
        /// Creates a single diagnostic test instance based on the provided test name and adds it to the dictionary of available tests.
        /// </summary>
        /// <param name="testName">The name of the diagnostic test to create.</param>
        public abstract void AddDiagnosticTest(string testName);
        /// <summary>
        /// Creates all available diagnostic tests for a specific hardware component and adds them to the dictionary of available tests.
        /// </summary>
        public abstract void AddAllDiagnosticTests();
        /// <summary>
        /// Gets a dictionary containing the available diagnostic tests with their names as keys and their corresponding IDiagnosticTest objects as values.
        /// </summary>
        /// <returns>A dictionary of available diagnostic tests for the hardware component.</returns>
        public Dictionary<string, IDiagnosticTest> GetDiagnosticTests()
        {
            return DiagnosticTests;
        }

        /// <summary>
        /// Creates a single diagnostic test instance based on the provided test name.
        /// </summary>
        /// <param name="testName">The name of the diagnostic test to create.</param>
        /// <returns>An instance of the diagnostic test with the specified name, or null if the test name is not recognized.</returns>
        protected abstract IDiagnosticTest? CreateDiagnosticTest(string testName);
    }
}