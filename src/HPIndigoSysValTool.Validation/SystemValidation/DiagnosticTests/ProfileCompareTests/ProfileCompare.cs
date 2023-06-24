using HPIndigoSysValTool.Handlers.Logger;
using HPIndigoSysValTool.Validation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;

namespace HPIndigoSysValTool.Validation
{
    /// <summary>
    /// Implements a profile comparison diagnostic test for hardware components of type T.
    /// </summary>
    /// <typeparam name="T">The hardware component type.</typeparam>
    public class ProfileCompare<T> : DiagnosticTestBase<T>, IComplexDiagnosticTest<T>
    {
        private JObject _jsonObject;
        private string _jsonSerializeObject;
        private string _errorMessage;
        private string _profileName;
        private readonly JsonProfileLoader _jsonProfileLoader = JsonProfileLoader.Instance;

        public ProfileCompare(string testName) : base(testName)
        {
            //TestDescription = "This diagnostic test compares the hardware profile of the given components with a predefined profile.";
            TestDescription = "This test compares the current component configuration with the approved component profile.";

        }
        /// <summary>
        /// Load hardware profile for the given machine series, model and profile name.
        /// </summary>
        /// <param name="componentList"></param>
        /// <param name="profileName">The name of the profile to be loaded.</param>
        public void SetParameters(IList<T> componentList, string profileName)
        {
            try
            {
                this._profileName = profileName;
                string jsonFilePath = _jsonProfileLoader.GetJsonProfileFilePath(PressConfiguration.Instance.Series, PressConfiguration.Instance.Model, profileName);

                if (!File.Exists(jsonFilePath))
                {
                    throw new FileNotFoundException($"Hardware profile file not found: {jsonFilePath}");
                }

                LoggerProvider.FileLogger.Information($"Loading hardware profile file: {jsonFilePath}");
                _jsonObject = _jsonProfileLoader.LoadJsonProfile(jsonFilePath);


                this._components = componentList;
                _jsonSerializeObject = JsonConvert.SerializeObject(this._components, Formatting.Indented);

            }
            catch (ArgumentNullException ex)
            {
                LoggerProvider.FileLogger.Error($"ExecutionFailed getting hardware profile file, ArgumentNullException. (machineSeries - {PressConfiguration.Instance.Series}), (model - {PressConfiguration.Instance.Model}), (profileName - {profileName})");
#if DEBUG
                LoggerProvider.FileLogger.Error(ex.Message);
#endif
                throw;
            }
            catch (FileNotFoundException ex)
            {
                LoggerProvider.FileLogger.Error($"ExecutionFailed getting hardware profile file, FileNotFoundException.");
#if DEBUG
                LoggerProvider.FileLogger.Error(ex.Message);
#endif
                throw;
            }
        }


        protected override bool RunTest()
        {
            try
            {
                ReportProgress("Loading and comparing hardware profiles...");


                // Implement the logic for running the test using the _components and _jsonObject.
                var jsonComponentsObject = new JObject();
                jsonComponentsObject["Profile Details"] = JArray.FromObject(_components);

                var comparisonResult = new JObjectComparisonResult();
                comparisonResult.CompareJObjects(_jsonObject, jsonComponentsObject);
                var differences = comparisonResult.Differences;

                Details.Add("Differences", differences);
                Details.Add($"{this._profileName} Info", jsonComponentsObject["Profile Details"]!);
                Details.Add("Json Profile", _jsonObject);

                return differences.Count == 0;
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error(
                    $"An error occurred while running the profile comparison diagnostic test: {ex.Message}");
                return false;
            }
        }

        public override string TestDescription { get; set; }
    }
}
