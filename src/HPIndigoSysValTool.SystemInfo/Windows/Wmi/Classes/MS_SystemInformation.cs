using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class MS_SystemInformation : WmiBase
{
    public string BIOSVersion => GetPropertyValue<string>(nameof(BIOSVersion)) ?? string.Empty;

    public string BIOSReleaseDate => GetPropertyValue<string>(nameof(BIOSReleaseDate)) ?? string.Empty;

    public static List<MS_SystemInformation> Get()
    {
        return Get<MS_SystemInformation>("\\ROOT\\WMI", nameof(MS_SystemInformation));
    }
}