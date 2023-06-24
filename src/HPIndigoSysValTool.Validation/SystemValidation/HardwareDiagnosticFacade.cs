using HPIndigoSysValTool.Handlers.Logger;
using HPIndigoSysValTool.SystemInfo;

namespace HPIndigoSysValTool.Validation;

/// <summary>
///     Facade class for managing hardware diagnostic tests.
/// </summary>
public class HardwareDiagnosticFacade
{
    private static readonly Lazy<HardwareDiagnosticFacade> _instance = new(() => new HardwareDiagnosticFacade());
    private readonly IHardwareDiagnosticFactory _cpuDiagnosticFactory;
    private readonly Dictionary<string, IHardwareDiagnosticFactory> _diagnosticFactories;
    private readonly DiagnosticStateManager _stateManager;
    private readonly SysInfo _systemInfo;

    // Dictionary to store test instances
    private readonly Dictionary<string, IDiagnosticTest> _tests;

    /// <summary>
    ///     Creates a new instance of the HardwareDiagnosticFacade class.
    /// </summary>
    private HardwareDiagnosticFacade()
    {
        _systemInfo = SysInfo.Instance;

        _diagnosticFactories = new Dictionary<string, IHardwareDiagnosticFactory>
        {
            { "Processor", new ProcessorDiagnosticFactory(_systemInfo) },
            { "Memory", new MemoryDiagnosticFactory(_systemInfo) },
            { "Storage", new StorageDiagnosticFactory(_systemInfo) },
            { "Video", new VideoDiagnosticFactory(_systemInfo) },
            { "Vcron", new VcronDiagnosticFactory(_systemInfo) },
            { "SystemInformation", new SystemInformationDiagnosticFactory(_systemInfo) },
            { "USB", new USBDiagnosticFactory(_systemInfo) },
            { "NetworkCard", new NetworkDiagnosticFactory(_systemInfo) },

        };

        _stateManager = new DiagnosticStateManager();

        // Initialize tests dictionary
        _tests = new Dictionary<string, IDiagnosticTest>();
    }

    public static HardwareDiagnosticFacade Instance => _instance.Value;

    /// <summary>
    ///     Runs a specified diagnostic test on the specified hardware component and returns the combined results of the
    ///     current and previous tests.
    /// </summary>
    /// <param name="testName">The name of the diagnostic test to run.</param>
    /// <param name="hardwareComponentName">The name of the hardware component to run the diagnostic test on.</param>
    /// <returns>A DiagnosticTestResult object representing the combined results of the current and previous tests.</returns>
    public DiagnosticTestResult RunDiagnosticTest(string testName, string hardwareComponentName)
    {
        if (!_diagnosticFactories.TryGetValue(hardwareComponentName, out var factory))
        {
            LoggerProvider.FileLogger.Error($"Invalid hardware component name: {hardwareComponentName}");
            return null;
        }

        var previousState = _stateManager.LoadState(testName);

        if (IsTestReadyToRun(testName, hardwareComponentName, previousState))
        {
            factory.AddDiagnosticTest(testName);
            var test = factory.DiagnosticTests[testName];

            // Store test instance in the dictionary
            if (!_tests.ContainsKey(testName))
            {
                _tests.Add(testName, test);
            }

            var testResult = test.Run();
            testResult.TestName = testName;
            testResult.TestID = Guid.NewGuid().ToString();
            testResult.HardwareComponent = Enum.Parse<HardwareComponent>(hardwareComponentName);

            if (testResult.State == TestState.Running)
            {
                testResult.State = TestState.Running;
                _stateManager.SaveState(new DiagnosticTestState(testName, testResult));
            }

            if (testResult.State == TestState.ExecutionPassed)
            {
                testResult.State = TestState.ExecutionPassed;
                testResult.IsPassed = true;
                _stateManager.SaveDiagnosticTestResults(testName, testResult);
                LoggerProvider.FileLogger.Information(
                    $"Test {testName} on {hardwareComponentName} completed successfully.");
            }
            else
            {
                testResult.State = TestState.ExecutionFailed;
                _stateManager.SaveDiagnosticTestResults(testName, testResult);
                LoggerProvider.FileLogger.Error($"Test {testName} on {hardwareComponentName} failed.");
            }

            return testResult;
        }

        return null;
    }

    public string GetTestDescription(string testName, string hardwareComponentName)
    {
        if (!_tests.TryGetValue(testName, out var test))
        {
            // If the test is not found in the dictionary, create a test instance
            if (!_diagnosticFactories.TryGetValue(hardwareComponentName, out var factory))
            {
                LoggerProvider.FileLogger.Error($"Invalid hardware component name: {hardwareComponentName}");
                return "Test not found.";
            }

            factory.AddDiagnosticTest(testName);
            test = factory.DiagnosticTests[testName];

            // Store test instance in the dictionary
            _tests[testName] = test;
        }

        return test.TestDescription;
    }

    private bool IsTestReadyToRun(string testName, string hardwareComponentName, DiagnosticTestState previousState)
    {
        if (previousState != null && previousState.TestName == testName && previousState.HardwareComponent ==
            Enum.Parse<HardwareComponent>(hardwareComponentName))
        {
            LoggerProvider.FileLogger.Information(
                $"Loading previous test state for {testName} on {hardwareComponentName}...");

            if (previousState.State == TestState.Running)
            {
                LoggerProvider.FileLogger.Error($"Test {testName} on {hardwareComponentName} is already in progress.");
                return false;
            }

            if (previousState.State == TestState.ExecutionPassed)
            {
                LoggerProvider.FileLogger.Error(
                    $"Test {testName} on {hardwareComponentName} has already been completed.");
                return false;
            }

            if (previousState.State == TestState.ExecutionFailed)
            {
                LoggerProvider.FileLogger.Error(
                    $"Test {testName} on {hardwareComponentName} previously encountered an error.");
                return false;
            }
        }

        return true;
    }
}