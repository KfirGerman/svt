#nullable enable
using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class Win32_NetworkAdapterConfiguration : WmiBase
{
    public string? DefaultIPGateway => GetPropertyValue<string>(nameof(DefaultIPGateway)) ?? string.Empty;
    public bool? DHCPEnabled => GetPropertyValue<bool>(nameof(DHCPEnabled));
    public string? DHCPServer => GetPropertyValue<string>(nameof(DHCPServer)) ?? string.Empty;
    public string? DNSServerSearchOrder => GetPropertyValue<string>(nameof(DNSServerSearchOrder)) ?? string.Empty;
    public string? IPAddress => GetPropertyValue<string>(nameof(IPAddress)) ?? string.Empty;
    public string? IPSubnet => GetPropertyValue<string>(nameof(IPSubnet)) ?? string.Empty;
    public string? MACAddress => GetPropertyValue<string>(nameof(MACAddress)) ?? string.Empty;
    public string? SettingID => GetPropertyValue<string>(nameof(SettingID)) ?? string.Empty;

    public static List<Win32_NetworkAdapterConfiguration> Get()
    {
        return Get<Win32_NetworkAdapterConfiguration>("\\ROOT\\CIMV2", "win32_NetworkAdapterConfiguration");
    }
}