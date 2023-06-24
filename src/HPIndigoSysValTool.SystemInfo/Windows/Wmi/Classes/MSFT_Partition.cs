#nullable enable
using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class MSFT_Partition : WmiBase
{
    private string? volumePath;

    public string[] AccessPaths => GetPropertyValue<string[]>(nameof(AccessPaths)) ?? Array.Empty<string>();

    public string? DiskId => GetPropertyValue<string>(nameof(DiskId)) ?? string.Empty;
    public string? Guid => GetPropertyValue<string>(nameof(Guid)) ?? string.Empty;

    public uint? DiskNumber => GetPropertyValue<uint>(nameof(DiskNumber));
    public uint? PartitionNumber => GetPropertyValue<uint>(nameof(PartitionNumber));
    public ulong? Size => GetPropertyValue<ulong>(nameof(Size));
    public bool? IsBoot => GetPropertyValue<bool>(nameof(IsBoot));
    public string? DriveLetter => GetPropertyValue<string>(nameof(DriveLetter)) ?? string.Empty;
    public bool? NoDefaultDriveLetter => GetPropertyValue<bool>(nameof(NoDefaultDriveLetter));

    public bool? IsReadOnly => GetPropertyValue<bool>(nameof(IsReadOnly));

    public bool? IsActive => GetPropertyValue<bool>(nameof(IsActive));

    public bool? IsSystem => GetPropertyValue<bool>(nameof(IsSystem));

    public bool? IsHidden => GetPropertyValue<bool>(nameof(IsHidden));

    public bool? IsOffline => GetPropertyValue<bool>(nameof(IsOffline));

    public static List<MSFT_Partition> Get()
    {
        return Get<MSFT_Partition>("\\root\\Microsoft\\windows\\storage", "msft_partition");
    }
}