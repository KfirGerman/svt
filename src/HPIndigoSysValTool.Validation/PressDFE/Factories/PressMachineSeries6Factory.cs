using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.Handlers.Logger;

namespace HPIndigoSysValTool.Validation;

public class PressMachineSeries6Factory : PressMachineSeriesFactory
{
    /// <summary>
    ///     Creates a PressMachine object for the Series 6 press machines based on the provided model.
    /// </summary>
    /// <param name="model">The model of the press machine.</param>
    /// <returns>An instance of a PressMachine derived class, or null if the model is not supported.</returns>
    public override AbstractPressMachine CreatePressMachine(string model)
    {
        try
        {
            switch (model)
            {
                case "V12":
                    return null;
                default:
                    LoggerProvider.FileLogger.Error($"ExecutionFailed: Unsupported Press PC model {model} for Series 6.");
                    return null;
            }
        }
        catch (Exception ex)
        {
            LoggerProvider.FileLogger.Error($"ExecutionFailed creating PressMachine object for Series 6: {ex.Message}");
            throw;
        }
    }
}