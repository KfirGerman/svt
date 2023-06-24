using Newtonsoft.Json.Linq;

namespace HPIndigoSysValTool.Validation
{
    public static class DiagnosticTestResultExtensions
    {
        /// <summary>
        /// Converts a JObject to a DiagnosticTestResult object.
        /// </summary>
        /// <param name="result">The DiagnosticTestResult object to convert to.</param>
        /// <param name="jObject">The JObject to convert from.</param>
        /// <returns>The converted DiagnosticTestResult object.</returns>
        public static DiagnosticTestResult FromJObject(this DiagnosticTestResult result, JObject jObject)
        {
            try
            {
                result.TestID = jObject.Value<string>("TestID");
                result.StartTime = jObject.Value<DateTime>("StartTime");
                result.EndTime = jObject.Value<DateTime>("EndTime");
                result.Duration = jObject.Value<TimeSpan>("Duration");
                result.State = (TestState)jObject.Value<int>("Status");
                result.Details = jObject.Value<IDictionary<string, object>>("Details");
                return result;
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                throw new ArgumentException("Invalid JObject argument", nameof(jObject), ex);
            }
        }

        /// <summary>
        /// Converts a DiagnosticTestResult object to a JObject.
        /// </summary>
        /// <param name="result">The DiagnosticTestResult object to convert from.</param>
        /// <returns>The converted JObject.</returns>
        public static JObject ToJObject(this DiagnosticTestResult result)
        {
            try
            {
                var obj = new JObject
                {
                    { "TestID", result.TestID },
                    { "StartTime", result.StartTime },
                    { "EndTime", result.EndTime },
                    { "Duration", result.Duration },
                    { "Status", result.State.ToString() }
                };

                if (result.Details != null)
                {
                    var detailsObj = JObject.FromObject(result.Details);
                    obj.Add("Details", detailsObj);
                }

                return obj;
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                throw new ArgumentException("Invalid DiagnosticTestResult argument", nameof(result), ex);
            }
        }
    }
}
