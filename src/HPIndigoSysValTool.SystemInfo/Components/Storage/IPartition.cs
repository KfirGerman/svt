namespace HPIndigoSysValTool.SystemInfo.Components;

public interface IPartition
{
    uint? DiskNumber { get; set; }
    string? DriveLetter { get; set; }
    bool? IsActive { get; set; }
    bool? IsBoot { get; set; }
    bool? IsHidden { get; set; }
    bool? IsSystem { get; set; }
    uint? PartitionNumber { get; set; }
    string? VolumeLetter { get; set; }
    string? FileSystem { get; set; }
    string? VolumeLabel { get; set; }
    string? Size { get; set; }
    string? SizeRemaining { get; set; }
}