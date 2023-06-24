namespace HPIndigoSysValTool.Handlers.Json;


using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using HPIndigoSysValTool.Handlers.Logger;

/// <summary>
/// Class for handling JSON files
/// </summary>
public sealed class JsonFileHandler : IJsonFileHandler
{
    private readonly object _lock = new object();
    private static readonly Lazy<JsonFileHandler> _instance = new Lazy<JsonFileHandler>(() => new JsonFileHandler());

    private JsonFileHandler() { }

    public static JsonFileHandler Instance => _instance.Value;

    /// <summary>
    /// Loads a JSON file from the specified file path
    /// </summary>
    /// <param name="filePath">The file path of the JSON file</param>
    /// <returns>The JObject representation of the JSON file</returns>
    public JObject LoadJson(string filePath)
    {
        try
        {
            JObject result;

            lock (_lock)
            {
                using (var file = File.OpenText(filePath))
                {
                    using (var reader = new JsonTextReader(file))
                    {
                        result = JObject.Load(reader);
                    }
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            LoggerProvider.FileLogger.Error("Error loading JSON file: " + filePath);
#if DEBUG
            LoggerProvider.FileLogger.Error(ex.Message);
#endif

            return null;
        }
    }

    /// <summary>
    /// Saves a JObject to a JSON file at the specified file path
    /// </summary>
    /// <param name="filePath">The file path of the JSON file</param>
    /// <param name="data">The JObject data to be saved</param>
    public void SaveJson(string filePath, JObject data)
    {
        try
        {
            lock (_lock)
            {
                using (var file = File.CreateText(filePath))
                {
                    using (var writer = new JsonTextWriter(file))
                    {
                        data.WriteTo(writer);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggerProvider.FileLogger.Error("Error saving JSON file: " + filePath);
#if DEBUG
            LoggerProvider.FileLogger.Error(ex.Message);
#endif
        }
    }

    /// <summary>
    /// Reads a JSON file from the specified file path and converts it to a specified object type
    /// </summary>
    /// <typeparam name="T">The type to convert the JSON data to</typeparam>
    /// <param name="filePath">The file path of the JSON file</param>
    /// <returns>The object representation of the JSON data</returns>
    public T ReadJson<T>(string filePath)
    {
        try
        {
            lock (_lock)
            {
                using (var file = File.OpenText(filePath))
                {
                    JToken jsonData = JToken.ReadFrom(new JsonTextReader(file));
                    T result = jsonData.ToObject<T>();
                    return result;
                }
            }
        }
        catch (Exception ex)
        {
            LoggerProvider.FileLogger.Error("Error reading JSON file: " + filePath);
#if DEBUG
            LoggerProvider.FileLogger.Error(ex.Message);
#endif

            return default(T);
        }
    }
}