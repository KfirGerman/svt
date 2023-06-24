using HPIndigoSysValTool.Handlers.Logger;
using Microsoft.Win32;
using HPIndigoSysValTool.SystemInfo;

namespace HPIndigoSysValTool.Validation
{
    /// <summary>
    /// The RegistryReader class reads the values of the Model and Series of a press machine from the registry.
    /// </summary>
    public class RegistryReader : IRegistryReader
    {
        private const string ModelRegistryKey = @"SOFTWARE\MyCompany\MyApp\Model";
        private const string SeriesRegistryKey = @"SOFTWARE\MyCompany\MyApp\Series";

        /// <summary>
        /// Gets the Model of the press machine from the registry.
        /// </summary>
        /// <returns>The Model of the press machine as a string.</returns>
        public string? GetModelFromRegistry()
        {
            return ReadRegistryValue(ModelRegistryKey);
        }

        /// <summary>
        /// Gets the Series of the press machine from the registry.
        /// </summary>
        /// <returns>The Series of the press machine as a string.</returns>
        public string? GetSeriesFromRegistry()
        {
            return ReadRegistryValue(SeriesRegistryKey);
        }

        private string? ReadRegistryValue(string registryKey)
        {
            try
            {
                using var key = Registry.LocalMachine.OpenSubKey(registryKey);
                if (key != null)
                {
                    return key.GetValue(null)?.ToString();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error($"ExecutionFailed reading registry value for {registryKey}");
#if DEBUG
                LoggerProvider.FileLogger.Error(ex.Message);
#endif
            }

            return null;
        }
    }
}