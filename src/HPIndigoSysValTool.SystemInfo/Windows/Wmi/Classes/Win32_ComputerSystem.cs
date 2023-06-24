#nullable enable
using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class Win32_ComputerSystem : WmiBase
{
    public string? Name => GetPropertyValue<string>(nameof(Name)) ?? string.Empty;
    public string? Manufacturer => GetPropertyValue<string>(nameof(Manufacturer)) ?? string.Empty;
    public string? Model => GetPropertyValue<string>(nameof(Model)) ?? string.Empty;
    public string? SystemSKUNumber => GetPropertyValue<string>(nameof(SystemSKUNumber)) ?? string.Empty;
    public ulong? TotalPhysicalMemory => GetPropertyValue<ulong>(nameof(TotalPhysicalMemory));

    public static List<Win32_ComputerSystem> Get()
    {
        return Get<Win32_ComputerSystem>("\\ROOT\\CIMV2", "win32_ComputerSystem");
    }
}