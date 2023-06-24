using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.Validation;

using HPIndigoSysValTool.Handlers.Logger;

namespace HPIndigoSysValTool.Validation;

/// <summary>
/// Abstract factory for creating instances of <see cref="AbstractPressMachine"/> based on the press machine series.
/// </summary>
public abstract class PressMachineSeriesFactory : IPressMachineFactory
{
    /// <summary>
    /// Creates an instance of <see cref="AbstractPressMachine"/> with the given model.
    /// </summary>
    /// <param name="model">The model of the press machine to create.</param>
    /// <returns>An instance of <see cref="AbstractPressMachine"/> with the given model.</returns>
    public abstract AbstractPressMachine CreatePressMachine(string model);

    /// <summary>
    /// Creates an instance of the press machine based on the press machine series and model retrieved from <see cref="PressConfiguration"/>.
    /// </summary>
    /// <returns>An instance of the press machine.</returns>
    public virtual AbstractPressMachine CreatePressInstance()
    {
        var series = PressConfiguration.Instance.Series;
        var model = PressConfiguration.Instance.Model;

        if (string.IsNullOrEmpty(series) && string.IsNullOrEmpty(model))
        {
            LoggerProvider.FileLogger.Error("Could not retrieve press machine series or model from the registry.");
            LoggerProvider.FileLogger.Error("Creating default instance for Series3 - 6K.");
            return new PressPCSeries3_6K();
        }

        switch (series)
        {
            case "Series3":
                return new PressMachineSeries3Factory().CreatePressMachine(model);
            case "Series4":
                return new PressMachineSeries4Factory().CreatePressMachine(model);
            case "Series5":
                return new PressMachineSeries5Factory().CreatePressMachine(model);
            case "Series6":
                return new PressMachineSeries6Factory().CreatePressMachine(model);
            // Add other series cases here
            default:
                throw new ArgumentException($"Invalid series: {series}");
        }
    }
}