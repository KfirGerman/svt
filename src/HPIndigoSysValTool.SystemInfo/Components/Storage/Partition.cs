using System.ComponentModel;

namespace HPIndigoSysValTool.SystemInfo.Components;

public class Partition : IPartition
{
    [DisplayName("Disk Number")] public uint? DiskNumber { get; set; }

    [DisplayName("Drive Letter")] public string? DriveLetter { get; set; }

    [DisplayName("Is Active")] public bool? IsActive { get; set; }

    [DisplayName("Is Boot")] public bool? IsBoot { get; set; }

    [DisplayName("Is Hidden")] public bool? IsHidden { get; set; }

    [DisplayName("Is System")] public bool? IsSystem { get; set; }

    [DisplayName("Partition Number")] public uint? PartitionNumber { get; set; }

    [DisplayName("Volume Letter")] public string? VolumeLetter { get; set; }

    [DisplayName("File System")] public string? FileSystem { get; set; }

    [DisplayName("Volume Label")] public string? VolumeLabel { get; set; }

    [DisplayName("Size")] public string? Size { get; set; }

    [DisplayName("Size Remaining")] public string? SizeRemaining { get; set; }
}