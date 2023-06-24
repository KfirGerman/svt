using HPIndigoSysValTool.SystemInfo;
using HPIndigoSysValTool.SystemInfo.Components;
using System.Collections.Generic;

namespace HPIndigoSysValTool.Validation
{
    /// <summary>
    /// Represents a factory for creating diagnostic tests for SystemInformation components.
    /// </summary>
    public class SystemInformationDiagnosticFactory : HardwareDiagnosticFactoryBase
    {
        /// <summary>
        /// Initializes a new instance of the SystemInformationDiagnosticFactory class.
        /// </summary>
        /// <param name="systemInfo">The SystemInfo instance containing information about the system's hardware components.</param>
        public SystemInformationDiagnosticFactory(SysInfo systemInfo) : base(systemInfo)
        {
        }

        /// <summary>
        /// Adds a single diagnostic test to the dictionary of available tests.
        /// </summary>
        /// <param name="testName">The name of the diagnostic test to add.</param>
        public override void AddDiagnosticTest(string testName)
        {
            DiagnosticTests[testName] = CreateDiagnosticTest(testName);
        }

        /// <summary>
        /// Adds all available diagnostic tests to the dictionary of available tests.
        /// </summary>
        public override void AddAllDiagnosticTests()
        {
            // TODO: create a list of all available diagnostic tests for SystemInformation components and add them to the dictionary
        }

        /// <summary>
        /// Creates a single diagnostic test instance based on the provided test name.
        /// </summary>
        /// <param name="testName">The name of the diagnostic test to create.</param>
        /// <returns>An instance of the diagnostic test with the specified name, or null if the test name is not recognized.</returns>
        protected override IDiagnosticTest? CreateDiagnosticTest(string testName)
        {
            switch (testName.ToLower())
            {
                case "system information profile compare":
                    IList<ISystemInformation> _SystemInformationList = _systemInfo.SystemInformationList;
                    var profileCompare = new ProfileCompare<ISystemInformation>("System Information Profile Compare");
                    profileCompare.SetParameters(_SystemInformationList, "SystemInformation");
                    return profileCompare;
                default:
                    return null;
            }
        }
    }
}
