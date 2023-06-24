namespace HPIndigoSysValTool.SystemInfo.Components;

public interface IStorage
{
    string? Model { get; set; }
    string? Manufacturer { get; set; }
    string? Location { get; set; }
    string? SerialNumber { get; set; }
    string? DiskSize { get; set; }
    string? AllocatedSize { get; set; }
    string? DiskFreeSize { get; set; }
    string? BusType { get; set; }
    uint? Number { get; set; }
    uint? NumberOfPartitions { get; set; }
    bool? IsOffline { get; set; }
    bool? IsReadOnly { get; set; }
    bool? IsSystem { get; set; }
    bool? IsBoot { get; set; }
    bool? IsNvme { get; set; }
    bool? IsRaid { get; set; }
    List<IPartition> Partitions { get; set; }
}