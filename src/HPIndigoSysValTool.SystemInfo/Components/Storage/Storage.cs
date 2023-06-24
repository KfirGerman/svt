using System.ComponentModel;

namespace HPIndigoSysValTool.SystemInfo.Components;

[Flags]
public enum BusType
{
    Unknown = 0,
    SCSI = 1,
    ATAPI = 2,
    ATA = 3,
    IEEE1394 = 4,
    SSA = 5,
    FibreChannel = 6,
    USB = 7,
    RAID = 8,
    ISCSI = 9,
    SAS = 10,
    SATA = 11,
    SD = 12,
    MMC = 13,
    Virtual = 14,
    FileBackedVirtual = 15,
    StorageSpaces = 16,
    NVMe = 17
}

public class Storage : IStorage
{


    [DisplayName("Model")] public string? Model { get; set; }

    [DisplayName("Manufacturer")] public string? Manufacturer { get; set; }

    [DisplayName("Location")] public string? Location { get; set; }

    [DisplayName("Serial Number")] public string? SerialNumber { get; set; }

    [DisplayName("Disk Size")] public string? DiskSize { get; set; }

    [DisplayName("Allocated Size")] public string? AllocatedSize { get; set; }

    [DisplayName("Disk Free Size")] public string? DiskFreeSize { get; set; }

    [DisplayName("Bus Type")] public string? BusType { get; set; }

    [DisplayName("Number")] public uint? Number { get; set; }

    [DisplayName("Number of Partitions")] public uint? NumberOfPartitions { get; set; }

    [DisplayName("Is Offline")] public bool? IsOffline { get; set; }

    [DisplayName("Is Read-Only")] public bool? IsReadOnly { get; set; }

    [DisplayName("Is System")] public bool? IsSystem { get; set; }

    [DisplayName("Is Boot")] public bool? IsBoot { get; set; }

    [DisplayName("Is NVMe")] public bool? IsNvme { get; set; }

    [DisplayName("Is RAID")] public bool? IsRaid { get; set; }

    public List<IPartition> Partitions { get; set; }
}