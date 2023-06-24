#nullable enable
using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class Win32_NetworkAdapter : WmiBase
{
    public uint? Availability => GetPropertyValue<uint>(nameof(Availability));
    public string? Caption => GetPropertyValue<string>(nameof(Caption));
    public string? Description => GetPropertyValue<string>(nameof(Description)) ?? string.Empty;
    public string? DeviceID => GetPropertyValue<string>(nameof(DeviceID)) ?? string.Empty;
    public string? GUID => GetPropertyValue<string>(nameof(GUID)) ?? string.Empty;
    public uint? Index => GetPropertyValue<uint>(nameof(Index));
    public string? InstallDate => GetPropertyValue<string>(nameof(InstallDate));
    public bool? Installed => GetPropertyValue<bool>(nameof(Installed));
    public uint? InterfaceIndex => GetPropertyValue<uint>(nameof(InterfaceIndex));
    public string? MACAddress => GetPropertyValue<string>(nameof(MACAddress)) ?? string.Empty;
    public string? Manufacturer => GetPropertyValue<string>(nameof(Manufacturer)) ?? string.Empty;
    public uint? MaxNumberControlled => GetPropertyValue<uint>(nameof(MaxNumberControlled));
    public string? Name => GetPropertyValue<string>(nameof(Name)) ?? string.Empty;
    public string? NetConnectionID => GetPropertyValue<string>(nameof(NetConnectionID)) ?? string.Empty;
    public uint? NetConnectionStatus => GetPropertyValue<uint>(nameof(NetConnectionStatus));
    public bool? NetEnabled => GetPropertyValue<bool>(nameof(NetEnabled));
    public bool? PhysicalAdapter => GetPropertyValue<bool>(nameof(PhysicalAdapter));
    public string? PNPDeviceID => GetPropertyValue<string>(nameof(PNPDeviceID)) ?? string.Empty;
    public string? TimeOfLastReset => GetPropertyValue<string>(nameof(TimeOfLastReset));
    public string? ServiceName => GetPropertyValue<string>(nameof(ServiceName)) ?? string.Empty;


    public static List<Win32_NetworkAdapter> Get()
    {
        return Get<Win32_NetworkAdapter>("\\ROOT\\CIMV2", "win32_NetworkAdapter");
    }
}