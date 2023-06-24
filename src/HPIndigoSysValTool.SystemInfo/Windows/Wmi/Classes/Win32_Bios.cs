#nullable enable
using System.Runtime.Versioning;
using HPIndigoSysValTool.SystemInfo.Windows;

namespace HPIndigoSysValTool.SystemInfo
    .Wmi;

[SupportedOSPlatform("windows")]
public class Win32_Bios : WmiBase
{
    public string? SerialNumber => GetPropertyValue<string>(nameof(SerialNumber)) ?? string.Empty;

    public static List<Win32_Bios> Get()
    {
        return Get<Win32_Bios>("\\ROOT\\CIMV2", "win32_Bios");
    }
}