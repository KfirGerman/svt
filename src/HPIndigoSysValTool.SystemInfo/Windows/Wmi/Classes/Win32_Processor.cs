#nullable enable
using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class Win32_Processor : WmiBase
{
    public string? Caption => GetPropertyValue<string>(nameof(Caption)) ?? string.Empty;
    public string? DeviceID => GetPropertyValue<string>(nameof(DeviceID)) ?? string.Empty;
    public string? Description => GetPropertyValue<string>(nameof(Description)) ?? string.Empty;
    public string? SocketDesignation => GetPropertyValue<string>(nameof(SocketDesignation)) ?? string.Empty;
    public string? Manufacturer => GetPropertyValue<string>(nameof(Manufacturer)) ?? string.Empty;
    public string? Name => GetPropertyValue<string>(nameof(Name)) ?? string.Empty;
    public uint? NumberOfCores => GetPropertyValue<uint>(nameof(NumberOfCores));
    public uint? NumberOfLogicalProcessors => GetPropertyValue<uint>(nameof(NumberOfLogicalProcessors));
    public string? ProcessorId => GetPropertyValue<string>(nameof(ProcessorId)) ?? string.Empty;

    public static List<Win32_Processor> Get()
    {
        return Get<Win32_Processor>("\\ROOT\\CIMV2", "win32_Processor");
    }
}