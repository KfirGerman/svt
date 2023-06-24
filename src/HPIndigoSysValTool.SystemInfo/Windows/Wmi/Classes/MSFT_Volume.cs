#nullable enable
using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class MSFT_Volume : WmiBase
{
    private string? volumePath;

    public string? UniqueId => GetPropertyValue<string>(nameof(UniqueId)) ?? string.Empty;
    public string? DriveLetter => GetPropertyValue<string>(nameof(DriveLetter)) ?? string.Empty;
    public string? FileSystemLabel => GetPropertyValue<string>(nameof(FileSystemLabel)) ?? string.Empty;
    public string? FileSystem => GetPropertyValue<string>(nameof(FileSystem)) ?? string.Empty;
    public string? ObjectId => GetPropertyValue<string>(nameof(ObjectId)) ?? string.Empty;
    public string? Path => GetPropertyValue<string>(nameof(Path)) ?? string.Empty;
    public ulong? Size => GetPropertyValue<ulong>(nameof(Size));
    public ulong? SizeRemaining => GetPropertyValue<ulong>(nameof(SizeRemaining));

    public static List<MSFT_Volume> Get()
    {
        return Get<MSFT_Volume>("\\root\\Microsoft\\windows\\storage", "msft_volume");
    }
}