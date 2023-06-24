using System.Management;
using System.Runtime.Versioning;
using HPIndigoSysValTool.Handlers.Logger;
using HPIndigoSysValTool.SystemInfo.Components;
using HPIndigoSysValTool.SystemInfo.Helpers;

namespace HPIndigoSysValTool.SystemInfo.Windows;

/// <summary>
///     This class retrieves system information from various sources.
/// </summary>
[SupportedOSPlatform("windows")]
public class SystemInformation
{
    private static readonly object _lock = new();

    /// <summary>
    ///     Retrieves a list of system information objects.
    /// </summary>
    /// <returns>A list of system information objects.</returns>
    public static List<ISystemInformation> Get()
    {
        return GetSystemInformation();
    }

    private static List<ISystemInformation> GetSystemInformation()
    {
        // Log that we are getting base system information
        LoggerProvider.FileLogger.Information("Getting Base System Information");

        List<Win32_ComputerSystem> win32_compSys = null;
        List<MS_SystemInformation> ms_SystemInformation = null;
        List<Win32_OperatingSystem> win32_OperatingSystems = null;
        lock (_lock)
        {
            try
            {
                win32_compSys = Win32_ComputerSystem.Get();
                ms_SystemInformation = MS_SystemInformation.Get();
                win32_OperatingSystems = Win32_OperatingSystem.Get();
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error("Error getting System Information");
#if DEBUG
                LoggerProvider.FileLogger.Error(ex.Message);
#endif
                throw;
            }
        }

        // Select the first item from each list and create a new system information object
        var systemInfo = win32_compSys.Select(sys => new Components.SystemInformation
        {
            Name = sys.Name,
            Manufacturer = sys.Manufacturer,
            Model = sys.Model,
            SystemSKUNumber = sys.SystemSKUNumber,
            MemorySize = Converters.GetByteSizeString((ulong)sys.TotalPhysicalMemory),
            BIOSVersion = ms_SystemInformation.Select(x => x.BIOSVersion).FirstOrDefault(),
            BIOSReleaseDate = ms_SystemInformation.Select(x => x.BIOSReleaseDate).FirstOrDefault(),
            InstalledOSName = win32_OperatingSystems.Select(x => x.InstalledOSName).FirstOrDefault(),
            BuildVersion = win32_OperatingSystems.Select(x => x.BuildVersion).FirstOrDefault(),
            InstallDate = win32_OperatingSystems.Select(x => ManagementDateTimeConverter.ToDateTime(x.InstallDate))
                .FirstOrDefault().ToString(),
            LastBootUpTime = win32_OperatingSystems
                .Select(x => ManagementDateTimeConverter.ToDateTime(x.LastBootUpTime)).FirstOrDefault().ToString()
        }).Cast<ISystemInformation>().ToList();
        return systemInfo;
    }
}