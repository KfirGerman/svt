using System.Reflection;
using Newtonsoft.Json;

namespace HPIndigoSysValTool.Validation.TestHistory;

public class History : IHistory
{

    private object _logAccessLock = new object();

    [JsonProperty]
    private List<HistoryRecord> records = new List<HistoryRecord>();


    private History() { }

    public static History Instance { get; } = new History();


    private object syncLock = new object();

    //public Task Flush()
    //{
    //    return Task.Run(() =>
    //    {
    //        CommitToDisk();
    //    });
    //}


    //private void CommitToDisk(IEnumerable<HistoryRecord> historyRecords)
    //{
    //    lock (_logAccessLock)
    //    {
    //        foreach (var w in Writers)
    //        {
    //            try
    //            {
    //                w.WriteLog(historyRecords);
    //            }
    //            catch (Exception e)
    //            {
    //                Framework.Trace.Error(w.GetType().ToString() + "Failed.", e.StackTrace);
    //            }
    //        }

    //        CreateBackupLog();
    //        Platform.WriteLogFile(JsonConvert.SerializeObject(this), "", ".json");
    //    }
    //}

    //public void LogReading(IEnumerable<ITestResult> results)
    //{
    //    lock (records) // Must lock at this level since commit serializes the whole instance
    //    {
    //        //results.ForEach(AddData);           
    //        CommitToDisk(AddRecords(results));
    //    }
    //}






    /// <summary>
    /// Creates a backup log file
    /// </summary>        
    private void CreateBackupLog()
    {
        try
        {
            string ExecutablePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string logfile = Path.Combine(ExecutablePath, "HPIndigoSysValTool_TestHistory.json");
            //Create a backup logfile only if an original backup logfile exists
            if (!File.Exists(logfile)) return;

            //Validates if we can successfully deserialize the original logfile,
            //if fail this will throw an exception
            JsonConvert.DeserializeObject<History>(File.ReadAllText(logfile));

            //create a backup logfile
            string bkfile = logfile + ".bk";
            File.Copy(logfile, bkfile, overwrite: true);
        }
        catch (Exception)
        {
            //Failed to deserialized. Suppressed exception. Returns gracefully.
            return;
        }
    }




    public bool? GetLastResult(Guid id)
    {
        throw new NotImplementedException();
    }

    public void LogReading(IEnumerable<IDiagnosticTestResult> test)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<HistoryRecord> Records { get; }
}