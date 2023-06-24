namespace HPIndigoSysValTool.Validation;

public interface IPressMachineFactory
{
    /// <summary>
    /// Creates a PressMachine object based on the provided model.
    /// </summary>
    /// <param name="model">The model of the press machine.</param>
    /// <returns>An instance of a PressMachine derived class, or null if the model is not supported.</returns>
    AbstractPressMachine CreatePressMachine(string model);
}