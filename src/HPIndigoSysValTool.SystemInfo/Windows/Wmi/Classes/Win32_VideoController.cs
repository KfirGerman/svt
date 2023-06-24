#nullable enable
using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class Win32_VideoController : WmiBase
{
    public string? AdapterCompatibility => GetPropertyValue<string>(nameof(AdapterCompatibility)) ?? string.Empty;
    public string? Caption => GetPropertyValue<string>(nameof(Caption)) ?? string.Empty;
    public string? DeviceID => GetPropertyValue<string>(nameof(DeviceID)) ?? string.Empty;
    public string? PNPDeviceID => GetPropertyValue<string>(nameof(PNPDeviceID)) ?? string.Empty;
    public string? DriverDate => GetPropertyValue<string>(nameof(DriverDate)) ?? string.Empty;
    public string? DriverVersion => GetPropertyValue<string>(nameof(DriverVersion)) ?? string.Empty;
    public string? InstallDate => GetPropertyValue<string>(nameof(InstallDate)) ?? string.Empty;
    public uint? AdapterRAM => GetPropertyValue<uint>(nameof(AdapterRAM));

    public static List<Win32_VideoController> Get()
    {
        return Get<Win32_VideoController>("\\ROOT\\CIMV2", "Win32_VideoController");
    }
}