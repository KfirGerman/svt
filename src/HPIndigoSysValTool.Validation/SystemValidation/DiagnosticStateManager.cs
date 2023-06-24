using System;
using System.Collections.Generic;
using System.IO;
using HPIndigoSysValTool.Handlers.Json;
using HPIndigoSysValTool.Handlers.Logger;
using Newtonsoft.Json.Linq;

namespace HPIndigoSysValTool.Validation
{
    /// <summary>
    /// Manages the saving and loading of diagnostic test states.
    /// </summary>
    public class DiagnosticStateManager
    {
        private readonly JsonFileHandler _fileHandler;
        private readonly string _dataFolder = "C:\\SVT_Logs\\";

        public DiagnosticStateManager()
        {
            _fileHandler = JsonFileHandler.Instance;
            if (!Directory.Exists(_dataFolder))
            {
                Directory.CreateDirectory(_dataFolder);
            }
        }

        /// <summary>
        /// Saves the current state of a diagnostic test.
        /// </summary>
        /// <param name="state">The DiagnosticTestState object representing the current state of the diagnostic test.</param>
        public void SaveState(DiagnosticTestState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            string filePath = _dataFolder + state.TestName + ".json";
            try
            {
                _fileHandler.SaveJson(filePath, state.DiagnosticTestResult.ToJObject());
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error($"ExecutionFailed saving diagnostic test state for {state.TestName} to file {filePath}");
                LoggerProvider.FileLogger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Loads the most recent diagnostic test state for the specified test.
        /// </summary>
        /// <param name="testName">The name of the diagnostic test.</param>
        /// <returns>The most recent DiagnosticTestState object for the specified test, or null if no saved state is found.</returns>
        public DiagnosticTestState LoadState(string testName)
        {
            try
            {
                string filePath = _dataFolder + testName + ".json";
                if (!File.Exists(filePath))
                {
                    return null;
                }
                JObject json = _fileHandler.LoadJson(filePath);
                DiagnosticTestResult result = new DiagnosticTestResult();
                DiagnosticTestState state = new DiagnosticTestState(testName, result);
                state.DiagnosticTestResult.FromJObject(json);
                return state;
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error($"ExecutionFailed loading diagnostic test state for {testName} from folder {_dataFolder}");
                LoggerProvider.FileLogger.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Saves the diagnostic test results into a DATE_GUID.json file.
        /// </summary>
        /// <param name="testName">The name of the diagnostic test.</param>
        /// <param name="testResult">The DiagnosticTestResult object representing the results of the diagnostic test.</param>
        public void SaveDiagnosticTestResults(string testName, DiagnosticTestResult testResult)
        {
            if (testResult == null)
            {
                throw new ArgumentNullException(nameof(testResult));
            }

            string fileName = $"{testResult.HardwareComponent}_{testResult.TestID}.json";
            string filePath = Path.Combine(_dataFolder, fileName);
            try
            {
                _fileHandler.SaveJson(filePath, testResult.ToJObject());
                LoggerProvider.FileLogger.Information($"Diagnostic test results for {testName} saved to file {filePath}");
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error($"ExecutionFailed saving diagnostic test results for {testName} to file {filePath}");
                LoggerProvider.FileLogger.Error(ex.Message);
            }
        }

    }
}
