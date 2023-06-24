using Newtonsoft.Json.Linq;

namespace HPIndigoSysValTool.Validation
{
    /// <summary>
    /// Interface for loading JSON profiles
    /// </summary>
    public interface IJsonProfileLoader
    {
        /// <summary>
        /// Loads a JSON profile from the specified file path
        /// </summary>
        /// <param name="filePath">The file path of the JSON profile</param>
        /// <returns>The JObject representation of the JSON profile</returns>
        JObject? LoadJsonProfile(string filePath);

        /// <summary>
        /// Gets the file path for a JSON profile based on the machine series, model, and profile name
        /// </summary>
        /// <param name="machineSeries">The machine series of the profile</param>
        /// <param name="model">The model of the profile</param>
        /// <param name="profileName">The name of the profile</param>
        /// <returns>The file path of the JSON profile</returns>
        string GetJsonProfileFilePath(string? machineSeries, string? model, string profileName);
    }
}