using HPIndigoSysValTool.SystemInfo;
using HPIndigoSysValTool.SystemInfo.Components;
using System.Collections.Generic;

namespace HPIndigoSysValTool.Validation
{
    /// <summary>
    /// Represents a factory for creating diagnostic tests for processor components.
    /// </summary>
    public class ProcessorDiagnosticFactory : HardwareDiagnosticFactoryBase
    {
        /// <summary>
        /// Initializes a new instance of the ProcessorDiagnosticFactory class.
        /// </summary>
        /// <param name="systemInfo">The SystemInfo instance containing information about the system's hardware components.</param>
        public ProcessorDiagnosticFactory(SysInfo systemInfo) : base(systemInfo)
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
            // TODO: create a list of all available diagnostic tests for processor components and add them to the dictionary
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
                case "processor profile compare":
                    IList<IProcessor> _processorList = _systemInfo.ProcessorList;
                    var profileCompare = new ProfileCompare<IProcessor>("Processor Profile Compare");
                    profileCompare.SetParameters(_processorList, "Processor");
                    return profileCompare;
                default:
                    return null;
            }
        }
    }
}
