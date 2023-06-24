#nullable enable
using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class Win32_OperatingSystem : WmiBase
{
    public string? InstalledOSName => GetPropertyValue<string>("Caption") ?? string.Empty;
    public string? BuildVersion => GetPropertyValue<string>("Version") ?? string.Empty;
    public string? InstallDate => GetPropertyValue<string>(nameof(InstallDate)) ?? string.Empty;
    public string? LastBootUpTime => GetPropertyValue<string>(nameof(LastBootUpTime)) ?? string.Empty;

    public static List<Win32_OperatingSystem> Get()
    {
        return Get<Win32_OperatingSystem>("\\ROOT\\CIMV2", nameof(Win32_OperatingSystem));
    }
}