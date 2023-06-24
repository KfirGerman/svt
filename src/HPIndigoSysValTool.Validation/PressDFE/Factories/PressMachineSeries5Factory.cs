using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.Handlers.Logger;

namespace HPIndigoSysValTool.Validation;

public class PressMachineSeries5Factory : PressMachineSeriesFactory
{
    /// <summary>
    ///     Creates a PressMachine object for the Series 5 press machines based on the provided model.
    /// </summary>
    /// <param name="model">The model of the press machine.</param>
    /// <returns>An instance of a PressMachine derived class, or null if the model is not supported.</returns>
    public override AbstractPressMachine CreatePressMachine(string model)
    {
        try
        {
            switch (model)
            {
                case "100K":
                    return null;
                case "100K HD":
                    return null;
                case "200K":
                    return null;
                default:
                    LoggerProvider.FileLogger.Error($"ExecutionFailed: Unsupported Press PC model {model} for Series 5.");
                    return null;
            }
        }
        catch (Exception ex)
        {
            LoggerProvider.FileLogger.Error($"ExecutionFailed creating PressMachine object for Series 5: {ex.Message}");
            throw;
        }
    }
}