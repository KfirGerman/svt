using HPIndigoSysValTool.Handlers.Logger;
using HPIndigoSysValTool.SystemInfo.Components;

namespace HPIndigoSysValTool.SystemInfo.Windows;

public class ProcessorInformation
{
    private static readonly object _lock = new();

    public static List<IProcessor> Get()
    {
        return GetProcessorInfo();
    }

    private static List<IProcessor> GetProcessorInfo()
    {
        LoggerProvider.FileLogger.Information("Getting Processor Information.");

        List<Win32_Processor> win32_Processor = null;
        lock (_lock)
        {
            try
            {
                win32_Processor = Win32_Processor.Get();
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error("Error getting Processor Information");
                LoggerProvider.FileLogger.Error(ex.Message);

#if DEBUG
                LoggerProvider.FileLogger.Error(ex.StackTrace);
#endif
                throw;
            }
        }


        var processors = win32_Processor.Select(proc => new Processor
        {
            Caption = proc.Caption,
            DeviceID = proc.DeviceID,
            //Description = proc.Description,
            SocketDesignation = proc.SocketDesignation,
            Manufacturer = proc.Manufacturer,
            Name = proc.Name,
            NumberOfCores = proc.NumberOfCores,
            NumberOfLogicalProcessors = proc.NumberOfLogicalProcessors,
            //ProcessorId = proc.ProcessorId
        }).Cast<IProcessor>().ToList();

        return processors;
    }
}