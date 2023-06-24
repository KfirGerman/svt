using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using HPIndigoSysValTool.Handlers.Logger;
using HPIndigoSysValTool.SystemInfo.Components;
using HPIndigoSysValTool.SystemInfo.Helpers;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class StorageInformation
{
    // Constants for common string values
    private const string NOT_AVAILABLE = "N/A";
    private const string NVME_MODEL_REGEX = @"nvme";
    private const string RAID_MODEL_REGEX = @"raid";
    private static readonly object _lock = new();



    /// <summary>
    ///     Constructor for Storage.
    ///     Gets all the information for the disk.
    /// </summary>
    /// <returns></returns>
    public static List<IStorage> Get()
    {
        return GetDiskData();
    }

    /// <summary>
    ///     Get Disk Data, including partitions and volumes.
    /// </summary>
    /// <returns></returns>
    private static List<IStorage> GetDiskData()
    {
        LoggerProvider.FileLogger.Information("Getting Storage Information");

        List<MSFT_Disk>? msftDisk = null;
        lock (_lock)
        {
            try
            {
                msftDisk = MSFT_Disk.Get();
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error("Error getting Storage Information");
#if DEBUG
                LoggerProvider.FileLogger.Error(ex.Message);
#endif
                throw;
            }
        }

        var disks = msftDisk.Select(disk => new Storage
        {
            Model = disk.Model,
            Manufacturer = disk.Manufacturer,
            Location = disk.Location,
            SerialNumber = disk.SerialNumber,
            DiskSize = Converters.GetByteSizeString((ulong)disk.Size),
            AllocatedSize = Converters.GetByteSizeString((ulong)disk.AllocatedSize),
            BusType = disk.BusType.ToString(),
            Number = disk.Number,
            NumberOfPartitions = disk.NumberOfPartitions,
            IsOffline = disk.IsOffline,
            IsReadOnly = disk.IsReadOnly,
            IsSystem = disk.IsSystem,
            IsBoot = disk.IsBoot,
            Partitions = GetPartitionData(disk.Path, disk.Number),
            IsRaid = Regex.IsMatch(disk.Model, RAID_MODEL_REGEX, RegexOptions.IgnoreCase),
            IsNvme = Regex.IsMatch(disk.Model, NVME_MODEL_REGEX, RegexOptions.IgnoreCase)
        }).Cast<IStorage>().ToList();

        return disks;
    }


    /// <summary>
    ///     Get Partition Data, this is called from the GetDiskData method
    ///     Each disk has a list of partitions, this method gets the data for each partition, and adds it to the storage object
    ///     To get the partition data, we need to pass DiskId which is the path of the disk
    /// </summary>
    /// <param name="DiskId"></param>
    /// <returns></returns>
    private static List<IPartition> GetPartitionData(string diskId, uint? diskNumber)
    {
        LoggerProvider.FileLogger.Information("Getting Partition Information");

        List<MSFT_Partition> msftPartitions;
        List<MSFT_Volume> msftVolumes;
        lock (_lock)
        {
            try
            {
                msftPartitions = MSFT_Partition.Get();
                msftVolumes = MSFT_Volume.Get();
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error("Error getting Partition Information");
#if DEBUG
                LoggerProvider.FileLogger.Error(ex.Message);
#endif
                throw;
            }
        }

        var partitions = msftPartitions
            .Select(part =>
            {
                MSFT_Volume? volume = msftVolumes.FirstOrDefault(vol => vol.Path.Contains(part.Guid));
                return (Partition: part, Volume: volume);
            })
            .Where(tuple => tuple.Partition.DiskId.Equals(diskId) && tuple.Partition?.Size.GetValueOrDefault() > 0)
            .Select(tuple =>
            {
                var part = tuple.Partition;
                var volume = tuple.Volume;

                return new Partition
                {
                    DiskNumber = part.DiskNumber,
                    DriveLetter = part.DriveLetter,
                    IsActive = part.IsActive,
                    IsBoot = part.IsBoot,
                    IsHidden = part.IsHidden,
                    IsSystem = part.IsSystem,
                    PartitionNumber = part.PartitionNumber,
                    VolumeLetter = volume?.DriveLetter ?? NOT_AVAILABLE,
                    FileSystem = volume?.FileSystem ?? NOT_AVAILABLE,
                    VolumeLabel = volume?.FileSystemLabel ?? NOT_AVAILABLE,
                    Size = volume != null ? Converters.GetByteSizeString((ulong)volume.Size) : NOT_AVAILABLE,
                    SizeRemaining = volume != null ? Converters.GetByteSizeString((ulong)volume.SizeRemaining) : NOT_AVAILABLE
                };
            }).Cast<IPartition>().ToList();

        return partitions;

    }
}