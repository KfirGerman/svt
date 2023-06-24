using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.Handlers.Logger;

namespace HPIndigoSysValTool.Validation;

public class PressMachineSeries4Factory : PressMachineSeriesFactory
{
    /// <summary>
    ///     Creates a PressMachine object for the Series 4 press machines based on the provided model.
    /// </summary>
    /// <param name="model">The model of the press machine.</param>
    /// <returns>An instance of a PressMachine derived class, or null if the model is not supported.</returns>
    public override AbstractPressMachine CreatePressMachine(string model)
    {
        try
        {
            switch (model)
            {
                case "15K":
                    return null;
                case "15K HD":
                    return null;
                case "25K":
                    return null;
                case "25K HD":
                    return null;
                case "35K":
                    return null;
                default:
                    LoggerProvider.FileLogger.Error($"ExecutionFailed: Unsupported Press PC model {model} for Series 4.");
                    return null;
            }
        }
        catch (Exception ex)
        {
            LoggerProvider.FileLogger.Error($"ExecutionFailed creating PressMachine object for Series 4: {ex.Message}");
            throw;
        }
    }
}