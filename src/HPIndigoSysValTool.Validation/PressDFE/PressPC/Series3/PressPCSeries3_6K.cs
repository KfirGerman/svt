using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.Handlers.Logger;
using System;

namespace HPIndigoSysValTool.Validation
{
    public class PressPCSeries3_6K : AbstractPressMachine
    {
        // private readonly HardwareDiagnosticFacade _diagnosticFacade = HardwareDiagnosticFacade.Instance;

        /// <summary>
        /// Initializes a new instance of the PressPCSeries3_6K class.
        /// </summary>
        public PressPCSeries3_6K() : base("3", "6K")
        {
            LoggerProvider.FileLogger.Information($"PressPCSeries3_6K instance created.");
        }

        /// <summary>
        /// Runs the specified diagnostic test on the specified hardware component and returns the combined results of the current and previous tests.
        /// </summary>
        /// <param name="testName">The name of the diagnostic test to run.</param>
        /// <param name="hardwareComponentName">The name of the hardware component to run the diagnostic test on.</param>
        /// <returns>A DiagnosticTestResult object representing the combined results of the current and previous tests.</returns>
        public override DiagnosticTestResult RunDiagnosticTest(string testName, string hardwareComponentName)
        {
            try
            {
                DiagnosticTestResult result = _diagnosticFacade.RunDiagnosticTest(testName, hardwareComponentName);
                return result;
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error($"ExecutionFailed running diagnostics for PressPCSeries3_6K: {ex.Message}");
                // show error message to user with ex.Message in a popup
                throw;
            }
        }


        /// <summary>
        /// Gets a dictionary containing the available diagnostic tests with their names as keys and the corresponding hardware component names as values.
        /// </summary>
        /// <returns>A dictionary of available diagnostic tests for the press machine with the test name as the key and the hardware component name as the value.</returns>
        public override Dictionary<string, List<string>> GetDiagnosticTests()
        {
            var diagnosticTests = base.GetDiagnosticTests();

            diagnosticTests["Video"].Add("New Video Test");

            return diagnosticTests;
        }

    }
}