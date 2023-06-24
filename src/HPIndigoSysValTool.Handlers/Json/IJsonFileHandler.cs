using Newtonsoft.Json.Linq;

namespace HPIndigoSysValTool.Handlers.Json;

public interface IJsonFileHandler
{
    /// <summary>
    /// Loads a JSON file from the specified file path
    /// </summary>
    /// <param name="filePath">The file path of the JSON file</param>
    /// <returns>The JObject representation of the JSON file</returns>
    JObject LoadJson(string filePath);

    /// <summary>
    /// Saves a JObject to a JSON file at the specified file path
    /// </summary>
    /// <param name="filePath">The file path of the JSON file</param>
    /// <param name="data">The JObject data to be saved</param>
    void SaveJson(string filePath, JObject data);

    /// <summary>
    /// Reads a JSON file from the specified file path and converts it to a specified object type
    /// </summary>
    /// <typeparam name="T">The type to convert the JSON data to</typeparam>
    /// <param name="filePath">The file path of the JSON file</param>
    /// <returns>The object representation of the JSON data</returns>
    T ReadJson<T>(string filePath);
}