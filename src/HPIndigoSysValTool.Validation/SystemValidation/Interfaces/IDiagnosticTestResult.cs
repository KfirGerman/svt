using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace HPIndigoSysValTool.Validation;

public interface IDiagnosticTestResult
{
    string TestID { get; set; }
    HardwareComponent HardwareComponent { get; set; }
    DateTime StartTime { get; set; }
    DateTime EndTime { get; set; }
    TimeSpan Duration { get; set; }
    TestState State { get; set; }
    IDictionary<string, object> Details { get; set; }
    string TestName { get; set; }
    string TestCategory { get; set; }
    string ErrorMessage { get; set; }
    bool IsPassed { get; set; }
}

