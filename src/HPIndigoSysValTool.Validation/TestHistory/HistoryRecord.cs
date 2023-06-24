namespace HPIndigoSysValTool.Validation.TestHistory
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Represents a record of test execution history.
    /// </summary>
    public class HistoryRecord
    {
        private const string DATETIME_FORMAT = "yyyy-MM-ddThh:mm:ss";

        /// <summary>
        /// Formats a TimeSpan object as a string in the format "HH:mm:ss".
        /// </summary>
        /// <param name="et">The elapsed time.</param>
        /// <returns>The formatted elapsed time string.</returns>
        public static string FormatElapsedTime(TimeSpan et)
        {
            return $"{et.Days * 24 + et.Hours:D2}:{et.Minutes:D2}:{et.Seconds:D2}";
        }

        /// <summary>
        /// Gets the result type as an integer.
        /// </summary>
        public int ResultType => (int)Result;

        /// <summary>
        /// Gets the result code based on the test state.
        /// </summary>
        public int ResultCode
        {
            get
            {
                return Result switch
                {
                    TestState.ExecutionPassed => 0,
                    TestState.ExecutionFailed => 1,
                    TestState.Cancelled => 0,
                    _ => -1,
                };
            }
        }

        /// <summary>
        /// Gets or sets the start time for the application.
        /// </summary>
        public DateTime ApplicationStartTime { get; set; }

        /// <summary>
        /// Gets the formatted start time for the application.
        /// </summary>
        public string FormattedApplicationStartTime => ApplicationStartTime.ToString(DATETIME_FORMAT);

        /// <summary>
        /// Gets or sets the start time for the test.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets the formatted start time for the test.
        /// </summary>
        public string FormattedStartTime => StartTime.ToString(DATETIME_FORMAT);

        /// <summary>
        /// Gets or sets the stop time for the test.
        /// </summary>
        public DateTime StopTime { get; set; }

        /// <summary>
        /// Gets the formatted stop time for the test.
        /// </summary>
        public string FormattedStopTime => StopTime.ToString(DATETIME_FORMAT);

        /// <summary>
        /// Gets the elapsed time of the test.
        /// </summary>
        public TimeSpan ElapsedTime => StopTime.Subtract(StartTime);

        /// <summary>
        /// Gets the formatted elapsed time of the test.
        /// </summary>
        public string FormattedElapsedTime => FormatElapsedTime(ElapsedTime);

        /// <summary>
        /// Gets the formatted result of the test.
        /// </summary>
        public string FormattedResult => Result.ToString();

        /// <summary>
        /// Gets or sets the result of the test.
        /// </summary>
        public TestState Result { get; set; }

        /// <summary>
        /// Gets or sets additional information about the test.
        /// </summary>
        public string LogData { get; set; }

        /// <summary>
        /// Gets or sets the ID of the test.
        /// </summary>
        public string TestID { get; set; }

        /// <summary>
        /// Gets or sets the name of the test.
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// Gets or sets a strongly typed result of a test.
        /// </summary>
        public JObject MetaData { get; set; }

        /// <summary>
        /// Gets or sets the type of the metadata.
        /// </summary>
        public Type MetaDataType { get; set; }


        /// <summary>
        /// Gets or sets the hardware component related to the test.
        /// </summary>
        public HardwareComponent HardwareComponent { get; set; }
    }
}