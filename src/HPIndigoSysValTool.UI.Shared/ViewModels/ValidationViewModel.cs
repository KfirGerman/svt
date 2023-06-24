using HPIndigoSysValTool.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPIndigoSysValTool.UI.Shared.ViewModels
{
    /// <summary>
    /// ViewModel for handling press validation and diagnostic tests.
    /// </summary>
    public class ValidationViewModel : BaseViewModel
    {
        public ValidationViewModel(AbstractPressMachine pressMachine, PressConfiguration pressConfiguration)
        {
            PressMachine = pressMachine;
            PressConfiguration = pressConfiguration;
            _testResults = new Dictionary<string, DiagnosticTestResult>();
        }

        public PressConfiguration PressConfiguration { get; }
        public AbstractPressMachine PressMachine { get; }

        private Dictionary<string, DiagnosticTestResult> _testResults;
        public Dictionary<string, DiagnosticTestResult> TestResults
        {
            get => _testResults;
            set => SetProperty(ref _testResults, value);
        }

        /// <summary>
        /// Runs a specific diagnostic test and updates the TestResults property.
        /// </summary>
        /// <param name="testName">The name of the diagnostic test to run.</param>
        /// <param name="hardwareComponentName">The name of the hardware component to run the diagnostic test on.</param>
        /// <param name="progress">An optional progress reporter to report progress of the test execution.</param>
        public async Task RunSpecificDiagnosticTestAsync(string testName, string hardwareComponentName, IProgress<(int, string)> progress, Action<string> errorCallback)
        {
            try
            {
                var testResult = await Task.Run(() =>
                {
                    // Report progress at the beginning of the test execution
                    progress?.Report((0, $"Starting test {testName} on {hardwareComponentName}..."));

                    // Simulate a long-running test by waiting for 5 seconds
                    Thread.Sleep(5000);

                    // Run the diagnostic test
                    var result = PressMachine.RunDiagnosticTest(testName, hardwareComponentName);

                    // Report progress at the end of the test execution
                    progress?.Report((100, $"Test {testName} on {hardwareComponentName} completed successfully."));

                    return result;
                });

                TestResults[testName] = testResult;
            }
            catch (Exception ex)
            {
                errorCallback?.Invoke($"An error occurred: {ex.Message}");
            }
        }

        //public async Task RunSpecificDiagnosticTestAsync(string testName, string hardwareComponentName)
        //{
        //    var testResult = await Task.Run(() => PressMachine.RunDiagnosticTest(testName, hardwareComponentName));
        //    TestResults[testName] = testResult;
        //}

        /// <summary>
        /// Runs all available diagnostic tests one by one and updates the TestResults property.
        /// </summary>
        //public async Task RunAllDiagnosticTestsAsync()
        //{
        //    TestResults = new Dictionary<string, DiagnosticTestResult>();
        //    var diagnosticTests = GetAllAvailableTests();

        //    foreach (var test in diagnosticTests)
        //    {
        //        await RunSpecificDiagnosticTestAsync(test.Key, test.Value.ToString());
        //    }
        //}

        /// <summary>
        /// Gets a dictionary containing the available diagnostic tests with their names as keys and the corresponding hardware component names as values.
        /// </summary>
        /// <returns>A dictionary of available diagnostic tests for the press machine with the test name as the key and the hardware component name as the value.</returns>
        public Dictionary<string, List<string>> GetAllAvailableTests()
        {
            return PressMachine.GetDiagnosticTests();
        }

        /// <summary>
        /// Get the description of a specific test.
        /// </summary>
        /// <param name="testName"></param>
        /// <returns></returns>
        public string GetTestDescription(string testName, string hardwareComponentName)
        {
            return PressMachine.GetTestDescription(testName, hardwareComponentName);
        }

    }
}
