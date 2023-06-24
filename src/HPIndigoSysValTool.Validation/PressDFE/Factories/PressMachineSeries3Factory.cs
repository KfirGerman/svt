using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.Handlers.Logger;

namespace HPIndigoSysValTool.Validation;

public class PressMachineSeries3Factory : PressMachineSeriesFactory
{
    /// <summary>
    ///     Creates a PressMachine object for the Series 3 press machines based on the provided model.
    /// </summary>
    /// <param name="model">The model of the press machine.</param>
    /// <returns>An instance of a PressMachine derived class, or null if the model is not supported.</returns>
    public override AbstractPressMachine CreatePressMachine(string model)
    {
        try
        {
            switch (model)
            {
                case "6K":
                    return new PressPCSeries3_6K();
                case "7K":
                    return new PressPCSeries3_7K();
                case "8K":
                    return new PressPCSeries3_8K();
                case "7ECO":
                    return new PressPCSeries3_7ECO();
                default:
                    LoggerProvider.FileLogger.Error($"ExecutionFailed: Unsupported Press PC model {model} for Series 3.");
                    return null;
            }
        }
        catch (Exception ex)
        {
            LoggerProvider.FileLogger.Error($"ExecutionFailed creating PressMachine object for Series 3: {ex.Message}");
            throw;
        }
    }
}