using Newtonsoft.Json.Linq;

namespace HPIndigoSysValTool.Validation;

/// <summary>
/// Represents a complex diagnostic test that accepts a list of hardware components and a JSON object.
/// </summary>
/// <typeparam name="T">The hardware component type.</typeparam>
public interface IComplexDiagnosticTest<T>
{
    /// <summary>
    /// Sets the parameters required for the diagnostic test.
    /// </summary>
    /// <param name="components">A list of hardware components of type T.</param>
    /// <param name="profileName">A json profile name to load.</param>
    void SetParameters(IList<T> components, string profileName);
}