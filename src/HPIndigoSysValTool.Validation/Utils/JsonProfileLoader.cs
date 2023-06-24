using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using HPIndigoSysValTool.Handlers.Json;
using HPIndigoSysValTool.Handlers.Logger;

namespace HPIndigoSysValTool.Validation
{
    /// <summary>
    /// Class for loading JSON profiles
    /// </summary>
    public class JsonProfileLoader : IJsonProfileLoader
    {
        // A private field used to handle JSON files.
        private readonly JsonFileHandler _jsonFileHandler;

        private static JsonProfileLoader _instance;

        private JsonProfileLoader()
        {
            _jsonFileHandler = JsonFileHandler.Instance;
        }

        /// <summary>
        /// Gets the instance of the JsonProfileLoader class. Implements the Singleton pattern.
        /// </summary>
        public static JsonProfileLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JsonProfileLoader();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Loads a JSON profile from the specified file path
        /// </summary>
        /// <param name="filePath">The file path of the JSON profile</param>
        /// <returns>The JObject representation of the JSON profile</returns>
        public JObject? LoadJsonProfile(string filePath)
        {
            try
            {
                var jsonData = _jsonFileHandler.ReadJson<JObject>(filePath);

                if (jsonData == null)
                {
                    LoggerProvider.FileLogger.Error($"Failed to load JSON Profile file from path: {filePath}");
                    return null;
                }

                return jsonData;
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error($"Failed to load JSON Profile file from path: {filePath}.");
#if DEBUG
                LoggerProvider.FileLogger.Error($"{ex.Message}");
#endif
                return null;
            }
        }

        /// <summary>
        /// Gets the file path for a JSON profile based on the machine series, model, and profile name
        /// </summary>
        /// <param name="machineSeries">The machine series of the profile</param>
        /// <param name="model">The model of the profile</param>
        /// <param name="profileName">The name of the profile</param>
        /// <returns>The file path of the JSON profile</returns>
        public string GetJsonProfileFilePath(string? machineSeries, string? model, string profileName)
        {
            try
            {
                if (string.IsNullOrEmpty(machineSeries))
                {
                    throw new ArgumentNullException(nameof(machineSeries));
                }

                if (string.IsNullOrEmpty(model))
                {
                    throw new ArgumentNullException(nameof(model));
                }

                if (string.IsNullOrEmpty(profileName))
                {
                    throw new ArgumentNullException(nameof(profileName));
                }

                var profileFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Profiles");
                string folderPath = Path.Combine(profileFolder, machineSeries, model);
                string jsonFilePath = Path.Combine(folderPath, $"{profileName}.json");

                return jsonFilePath;
            }
            catch (Exception ex)
            {

                LoggerProvider.FileLogger.Error($"Failed to get JSON Profile file path for machine series '{machineSeries}', model '{model}', and profile name '{profileName}'.");

                #if DEBUG
                LoggerProvider.FileLogger.Error($"{ex.Message}");
                throw;
                #endif
                return string.Empty;
            }
        }
    }
}
