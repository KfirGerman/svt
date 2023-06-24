using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.Handlers.Logger;
using HPIndigoSysValTool.SystemInfo;
using System;
using System.Collections.Generic;

namespace HPIndigoSysValTool.Validation
{
    public abstract class AbstractPressMachine : IPressMachine
    {
        /// <summary>
        /// Gets or sets the series of the press machine.
        /// </summary>
        public string Series { get; set; }

        /// <summary>
        /// Gets or sets the model of the press machine.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Initializes a new instance of the AbstractPressMachine class.
        /// </summary>
        /// <param name="series">The series of the press machine.</param>
        /// <param name="model">The model of the press machine.</param>
        protected AbstractPressMachine(string series, string model)
        {
            try
            {
                Series = series;
                Model = model;
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error($"ExecutionFailed initializing AbstractPressMachine: {ex.Message}");
                throw;
            }
        }


        protected readonly HardwareDiagnosticFacade _diagnosticFacade = HardwareDiagnosticFacade.Instance;

        /// <summary>
        /// Runs the specified diagnostic test on the specified hardware component and returns the combined results of the current and previous tests.
        /// </summary>
        /// <param name="testName">The name of the diagnostic test to run.</param>
        /// <param name="hardwareComponentName">The name of the hardware component to run the diagnostic test on.</param>
        /// <returns>A DiagnosticTestResult object representing the combined results of the current and previous tests.</returns>
        public abstract DiagnosticTestResult RunDiagnosticTest(string testName, string hardwareComponentName);

        /// <summary>
        /// Gets a dictionary containing the available diagnostic tests with their names as keys and the corresponding hardware component names as values.
        /// </summary>
        /// <returns>A dictionary of available diagnostic tests for the press machine with the test name as the key and the hardware component name as the value.</returns>
        /// <summary>
        /// Gets a dictionary containing the available diagnostic tests with their names as keys and the corresponding hardware component names as values.
        /// </summary>
        /// <returns>A dictionary of available diagnostic tests for the press machine with the test name as the key and the hardware component name as the value.</returns>
        public virtual Dictionary<string, List<string>> GetDiagnosticTests()
        {
            // Initialize an empty dictionary to store the diagnostic tests
            var diagnosticTests = new Dictionary<string, List<string>>();

            // Add the diagnostic tests for the specific press machine implementation
            diagnosticTests.Add("SystemInformation", new List<string> { "System Information Profile Compare" });
            diagnosticTests.Add("Memory", new List<string> { "Memory Profile Compare", "Memory Test (MemTest)" });
            diagnosticTests.Add("Processor", new List<string> { "Processor Profile Compare" });
            diagnosticTests.Add("NetworkCard", new List<string> { "NIC Profile Compare" });
            diagnosticTests.Add("Storage", new List<string> { "Storage Profile Compare" });
            diagnosticTests.Add("Video", new List<string> { "Video Profile Compare" });
            diagnosticTests.Add("USB", new List<string> { "USB Profile Compare" });
            diagnosticTests.Add("Vcron", new List<string> { "Vcron Profile Compare" });

            return diagnosticTests;
        }

        /// <summary>
        /// Return the description of the test.
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="hardwareComponentName"></param>
        /// <returns></returns>
        public virtual string GetTestDescription(string testName, string hardwareComponentName)
        {
            return _diagnosticFacade.GetTestDescription(testName, hardwareComponentName);
        }
    }
}
