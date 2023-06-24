#nullable enable
using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class Win32_Volume : WmiBase
{
    private string? volumePath;

    public string? DeviceId => GetPropertyValue<string>("DeviceID") ?? string.Empty;

    public bool? BootVolume => GetPropertyValue<bool>(nameof(BootVolume));

    public bool? SystemVolume => GetPropertyValue<bool>(nameof(SystemVolume));

    public static List<Win32_Volume> Get()
    {
        return Get<Win32_Volume>("\\ROOT\\CIMV2", "win32_Volume");
    }
}