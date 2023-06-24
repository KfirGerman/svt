using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.Handlers.Logger;
using System;

namespace HPIndigoSysValTool.Validation
{
    public class PressPCSeries3_8K : AbstractPressMachine
    {
        private readonly HardwareDiagnosticFacade _diagnosticFacade = HardwareDiagnosticFacade.Instance;

        /// <summary>
        /// Initializes a new instance of the PressPCSeries3_8K class.
        /// </summary>
        /// <param name="diagnosticFacade">An instance of HardwareDiagnosticFacade.</param>
        public PressPCSeries3_8K() : base("3", "8K")
        {
            LoggerProvider.FileLogger.Information($"PressPCSeries3_8K instance created.");
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
                LoggerProvider.FileLogger.Error($"ExecutionFailed running diagnostics for PressPCSeries3_8K: {ex.Message}");
                throw;
            }
        }

    }
}