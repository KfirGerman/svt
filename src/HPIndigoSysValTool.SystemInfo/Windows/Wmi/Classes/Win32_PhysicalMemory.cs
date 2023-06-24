#nullable enable
using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class Win32_PhysicalMemory : WmiBase
{
    public string? BankLabel => GetPropertyValue<string>(nameof(BankLabel)) ?? string.Empty;
    public string? DeviceLocator => GetPropertyValue<string>(nameof(DeviceLocator)) ?? string.Empty;
    public string? Manufacturer => GetPropertyValue<string>(nameof(Manufacturer)) ?? string.Empty;
    public string? PartNumber => GetPropertyValue<string>(nameof(PartNumber)) ?? string.Empty;
    public string? SerialNumber => GetPropertyValue<string>(nameof(SerialNumber)) ?? string.Empty;
    public ulong? Capacity => GetPropertyValue<ulong>(nameof(Capacity));
    public uint? Speed => GetPropertyValue<uint>(nameof(Speed));
    public uint? SMBIOSMemoryType => GetPropertyValue<uint>(nameof(SMBIOSMemoryType));
    public uint? TypeDetail => GetPropertyValue<uint>(nameof(TypeDetail));

    public static List<Win32_PhysicalMemory> Get()
    {
        return Get<Win32_PhysicalMemory>("\\ROOT\\CIMV2", "Win32_PhysicalMemory");
    }
}