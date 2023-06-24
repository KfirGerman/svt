using HPIndigoSysValTool.Handlers.Logger;
using HPIndigoSysValTool.Validation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace HPIndigoSysValTool.Validation
{
    public abstract class DiagnosticTestBase<T> : IDiagnosticTest
    {
        protected string TestName { get; set; }
        protected Dictionary<string, object> Details { get; set; }
        protected IList<T> _components;
        protected bool _isRebootRequired = false;
        protected string _errorMessage;

        public DiagnosticTestBase(string testName)
        {
            TestName = testName;
            //Logger = LoggerProvider.FileLogger;
            Details = new Dictionary<string, object>();
        }

        /// <summary>
        /// Runs the profile comparison diagnostic test.
        /// </summary>
        /// <returns>A DiagnosticTestResult object with the results of the test.</returns>
        public virtual DiagnosticTestResult Run()
        {
            var startTime = DateTime.Now;
            var endTime = DateTime.Now;
            var duration = endTime - startTime;

            try
            {
                bool isPassed = RunTest();

                endTime = DateTime.Now;
                duration = endTime - startTime;

                return new DiagnosticTestResult
                {
                    TestID = Guid.NewGuid().ToString(),
                    TestName = TestName,
                    TestDescription = TestDescription,
                    StartTime = startTime,
                    EndTime = endTime,
                    Duration = duration,
                    State = isPassed ? TestState.ExecutionPassed : TestState.ExecutionFailed,
                    Details = Details,
                    IsPassed = isPassed,
                };
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error($"An error occurred while running the diagnostic test: {ex.Message}");
                endTime = DateTime.Now;
                duration = endTime - startTime;

                return new DiagnosticTestResult
                {
                    TestID = Guid.NewGuid().ToString(),
                    TestName = TestName,
                    TestDescription = TestDescription,
                    StartTime = startTime,
                    EndTime = endTime,
                    Duration = duration,
                    State = TestState.ExecutionFailed,
                    Details = new Dictionary<string, object>()
                    {
                        { "ErrorMessage", ex.Message },
                        { "StackTrace", ex.StackTrace }
                    },
                    IsPassed = false,
                };
            }
        }

        protected abstract bool RunTest();

        /// <summary>
        /// Determines if a reboot is required after running the diagnostic test.
        /// </summary>
        /// <returns>True if a reboot is required, false otherwise.</returns>
        public bool IsRebootRequired
        {
            get { return _isRebootRequired; }
            private set { _isRebootRequired = value; }
        }

        public abstract string TestDescription { get; set; }


        // Add a delegate for progress reporting
        public delegate void ProgressReportHandler(string progressMessage);

        // Add an event using the delegate
        public event ProgressReportHandler OnProgressReport;

        // A method to raise the event
        protected virtual void ReportProgress(string progressMessage)
        {
            OnProgressReport?.Invoke(progressMessage);
        }

    }
}