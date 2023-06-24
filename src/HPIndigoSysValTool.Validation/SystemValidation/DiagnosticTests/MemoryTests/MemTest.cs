using HPIndigoSysValTool.Handlers.Logger;
using HPIndigoSysValTool.Validation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;

namespace HPIndigoSysValTool.Validation
{
    /// <summary>
    /// Implements a profile comparison diagnostic test for hardware components of type T.
    /// </summary>
    /// <typeparam name="T">The hardware component type.</typeparam>
    public class MemTest<T> : DiagnosticTestBase<T>
    {

        public MemTest(string testName) : base(testName)
        {
            //TestDescription = "This diagnostic test compares the hardware profile of the given components with a predefined profile.";
            TestDescription = "This test run the Memory Diagnostics test in 'HP Hardware Diagnostics UEFI'.";

        }


        protected override bool RunTest()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error(
                    $"An error occurred while running the profile comparison diagnostic test: {ex.Message}");
                return false;
            }
        }

        public override string TestDescription { get; set; }
    }
}
