#nullable enable
using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class Win32_PnPSignedDriver : WmiBase
{
    public string? DeviceID => GetPropertyValue<string>(nameof(DeviceID)) ?? string.Empty;
    public string? HardWareID => GetPropertyValue<string>(nameof(HardWareID)) ?? string.Empty;
    public string? Location => GetPropertyValue<string>(nameof(Location)) ?? string.Empty;

    public static List<Win32_PnPSignedDriver> Get()
    {
        return Get<Win32_PnPSignedDriver>("\\ROOT\\CIMV2", "Win32_PnPSignedDriver");
    }
}