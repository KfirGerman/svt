#nullable enable
using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class Win32_SystemSlot : WmiBase
{
    public uint? BusNumber => GetPropertyValue<uint>(nameof(BusNumber));
    public string? SlotDesignation => GetPropertyValue<string>(nameof(SlotDesignation)) ?? string.Empty;

    public static List<Win32_SystemSlot> Get()
    {
        return Get<Win32_SystemSlot>("\\ROOT\\CIMV2", "Win32_SystemSlot");
    }
}