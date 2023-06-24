using HPIndigoSysValTool.SystemInfo;
using HPIndigoSysValTool.SystemInfo.Components;
using System.Collections.Generic;
using System.Diagnostics;

namespace HPIndigoSysValTool.Validation
{
    /// <summary>
    /// Represents a factory for creating diagnostic tests for memory components.
    /// </summary>
    public class MemoryDiagnosticFactory : HardwareDiagnosticFactoryBase
    {
        /// <summary>
        /// Initializes a new instance of the MemoryDiagnosticFactory class.
        /// </summary>
        /// <param name="systemInfo">The SystemInfo instance containing information about the system's hardware components.</param>
        public MemoryDiagnosticFactory(SysInfo systemInfo) : base(systemInfo)
        {
        }

        /// <summary>
        /// Adds a single diagnostic test to the dictionary of available tests.
        /// </summary>
        /// <param name="testName">The name of the diagnostic test to add.</param>
        public override void AddDiagnosticTest(string testName)
        {
            DiagnosticTests[testName] = CreateDiagnosticTest(testName);
            var test = DiagnosticTests[testName];

            var testType = test.GetType();
            Debug.WriteLine(testType);
            if (testType.IsGenericType)
            {
                // Access the TestDescription property using reflection
                var testDescriptionProperty = testType.GetProperty("TestDescription");
                if (testDescriptionProperty != null)
                {
                    string testDescription = testDescriptionProperty.GetValue(test) as string;
                    Debug.WriteLine(testDescription);   

                    // Perform your desired operations with the TestDescription value
                }
                else
                {
                    // Handle the case when the TestDescription property is not found
                }
            }
            else
            {
                // Handle the case when the test is not an instance of DiagnosticTestBase<T>
            }
        }


        /// <summary>
        /// Adds all available diagnostic tests to the dictionary of available tests.
        /// </summary>
        public override void AddAllDiagnosticTests()
        {
            // TODO: create a list of all available diagnostic tests for memory components and add them to the dictionary
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
                case "memory profile compare":
                    IList<IMemory> _memoryList = _systemInfo.MemoryList;
                    var profileCompare = new ProfileCompare<IMemory>("Memory Profile Compare");
                    profileCompare.SetParameters(_memoryList, "Memory");
                    return profileCompare;
                case "memory test (memtest)":
                    var memTest = new MemTest<IMemory>("Memory Test (MemTest)");

                    return memTest;
                default:
                    return null;
            }
        }
    }
}
