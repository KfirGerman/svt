using System.Runtime.Versioning;
using HPIndigoSysValTool.Handlers.Logger;
using HPIndigoSysValTool.SystemInfo.Components;
using HPIndigoSysValTool.SystemInfo.Helpers;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class MemoryInformation
{
    private static readonly object _lock = new();
    public static List<IMemory> Get()
    {
        return GetMemoryInformation();
    }

    private static List<IMemory> GetMemoryInformation()
    {
        LoggerProvider.FileLogger.Information("Getting Memory Information");


        List<Win32_PhysicalMemory> win32_PhysicalMemory = null;
        lock (_lock)
        {
            try
            {
                win32_PhysicalMemory = Win32_PhysicalMemory.Get();
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error("Error getting Memory Information");
                LoggerProvider.FileLogger.Error(ex.Message);

#if DEBUG
                LoggerProvider.FileLogger.Error(ex.StackTrace);
#endif
            }
        }

        var memories = win32_PhysicalMemory.Select(dimm => new Memory
        {
            BankLabel = dimm.BankLabel,
            DeviceLocator = dimm.DeviceLocator,
            Manufacturer = dimm.Manufacturer,
            PartNumber = dimm.PartNumber,
            SerialNumber = dimm.SerialNumber,
            Capacity = Converters.GetByteSizeString((ulong)dimm.Capacity),
            Speed = $"{dimm.Speed} MHz",
            SMBIOSMemoryType = ((SMBIOSMemoryType)dimm.SMBIOSMemoryType).ToString(),
            //TypeDetail = ((TypeDetail)dimm.TypeDetail).ToString()
        }).Cast<IMemory>().ToList();

        return memories;
    }
}