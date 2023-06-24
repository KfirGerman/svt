#nullable enable
using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class MSFT_Disk : WmiBase
{
    public string? Path => GetPropertyValue<string>(nameof(Path)) ?? string.Empty;

    public string? Location => GetPropertyValue<string>(nameof(Location)) ?? string.Empty;

    public string? FriendlyName => GetPropertyValue<string>(nameof(FriendlyName)) ?? string.Empty;

    public string? UniqueId => GetPropertyValue<string>(nameof(UniqueId)) ?? string.Empty;

    public ushort? UniqueIdFormat => GetPropertyValue<ushort>(nameof(UniqueIdFormat));

    public uint? Number => GetPropertyValue<uint>(nameof(Number));

    public string? SerialNumber => GetPropertyValue<string>(nameof(SerialNumber)) ?? string.Empty;

    public string? FirmwareVersion => GetPropertyValue<string>(nameof(FirmwareVersion)) ?? string.Empty;

    public string? Manufacturer => GetPropertyValue<string>(nameof(Manufacturer)) ?? string.Empty;

    public string? Model => GetPropertyValue<string>(nameof(Model)) ?? string.Empty;

    public ulong? Size => GetPropertyValue<ulong>(nameof(Size));

    public ulong? AllocatedSize => GetPropertyValue<ulong>(nameof(AllocatedSize));

    public uint? LogicalSectorSize => GetPropertyValue<uint>(nameof(LogicalSectorSize));

    public uint? PhysicalSectorSize => GetPropertyValue<uint>(nameof(PhysicalSectorSize));

    public ulong? LargestFreeExtent => GetPropertyValue<ulong>(nameof(LargestFreeExtent));

    public uint? NumberOfPartitions => GetPropertyValue<uint>(nameof(NumberOfPartitions));

    public ushort? ProvisioningType => GetPropertyValue<ushort>(nameof(ProvisioningType));

    public ushort? HealthStatus => GetPropertyValue<ushort>(nameof(HealthStatus));

    public ushort? BusType => GetPropertyValue<ushort>(nameof(BusType));

    public ushort? PartitionStyle => GetPropertyValue<ushort>(nameof(PartitionStyle));

    public uint? Signature => GetPropertyValue<uint>(nameof(Signature));

    public string? Guid => GetPropertyValue<string>(nameof(Guid)) ?? Guid;

    public bool? IsOffline => GetPropertyValue<bool>(nameof(IsOffline));

    public ushort? OfflineReason => GetPropertyValue<ushort>(nameof(OfflineReason));

    public bool? IsReadOnly => GetPropertyValue<bool>(nameof(IsReadOnly));

    public bool? IsSystem => GetPropertyValue<bool>(nameof(IsSystem));

    public bool? IsClustered => GetPropertyValue<bool>(nameof(IsClustered));

    public bool? IsBoot => GetPropertyValue<bool>(nameof(IsBoot));

    public bool? BootFromDisk => GetPropertyValue<bool>(nameof(BootFromDisk));

    public static List<MSFT_Disk> Get()
    {
        return Get<MSFT_Disk>("\\\\localhost\\ROOT\\Microsoft\\Windows\\Storage", nameof(MSFT_Disk));
    }
}