namespace HPIndigoSysValTool.Validation
{
    /// <summary>
    /// The IRegistryReader interface defines the methods to read registry values for the Model and Series of a press machine.
    /// </summary>
    public interface IRegistryReader
    {
        /// <summary>
        /// Gets the Model of the press machine from the registry.
        /// </summary>
        /// <returns>The Model of the press machine as a string.</returns>
        string? GetModelFromRegistry();

        /// <summary>
        /// Gets the Series of the press machine from the registry.
        /// </summary>
        /// <returns>The Series of the press machine as a string.</returns>
        string? GetSeriesFromRegistry();
    }
}