using HPIndigoSysValTool.Handlers.Logger;
using System;

namespace HPIndigoSysValTool.Validation
{
    /// <summary>
    /// Singleton class for storing the press series and model configuration.
    /// </summary>
    public class PressConfiguration
    {
        private static readonly Lazy<PressConfiguration> lazyInstance =
            new Lazy<PressConfiguration>(() => new PressConfiguration());

        /// <summary>
        /// The singleton instance of the PressConfiguration class.
        /// </summary>
        public static PressConfiguration Instance => lazyInstance.Value;

        /// <summary>
        /// The press series string.
        /// </summary>
        public string? Series { get; set; }

        /// <summary>
        /// The press model string.
        /// </summary>
        public string? Model { get; set; }

        private PressConfiguration()
        {
            try
            {
                RegistryReader registryReader = new RegistryReader();
                string? series = registryReader.GetSeriesFromRegistry();
                string? model = registryReader.GetModelFromRegistry();

                if (!string.IsNullOrEmpty(series) && !string.IsNullOrEmpty(model))
                {
                    Series = series;
                    Model = model;
                    LoggerProvider.FileLogger.Information($"Press configuration initialized.");
                    LoggerProvider.FileLogger.Information($"Press series: {Series}, Press model: {Model}");
                }
                else
                {
                    Series = "Series3";
                    Model = "6K";
                    LoggerProvider.FileLogger.Information($"Press configuration not initialized.");
                    LoggerProvider.FileLogger.Information($"Set default values - Press series: Series3, Press model: 6K");
                }

            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error($"ExecutionFailed initializing press configuration.");

#if DEBUG
                LoggerProvider.FileLogger.Error($"{ex.Message}");
#endif
            }
        }

        /// <summary>
        /// Updates the press series and model configuration.
        /// </summary>
        /// <param name="series">The new press series string.</param>
        /// <param name="model">The new press model string.</param>
        public void UpdateSeriesAndModel(string? series, string? model)
        {
            try
            {
                if (!string.IsNullOrEmpty(series) && !string.IsNullOrEmpty(model))
                {
                    Series = series;
                    Model = model;
                }

                LoggerProvider.FileLogger.Information($"Press configuration updated.");
                LoggerProvider.FileLogger.Information($"Press series: {Series}, Press model: {Model}");
            }
            catch (Exception ex)
            {
                // log error and continue
                LoggerProvider.FileLogger.Error($"ExecutionFailed updating press configuration.");
#if DEBUG
                LoggerProvider.FileLogger.Error($"{ex.Message}");
#endif
            }
        }
    }
}
