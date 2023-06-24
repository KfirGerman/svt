namespace HPIndigoSysValTool.SystemInfo.Components;

/// <summary>
///     Operating System Info
/// </summary>
public interface ISystemInformation
{
    string Name { get; set; }
    string Manufacturer { get; set; }
    string Model { get; set; }
    string SystemSKUNumber { get; set; }
    string MemorySize { get; set; }
    string BIOSVersion { get; set; }
    string BIOSReleaseDate { get; set; }
    string InstalledOSName { get; set; }
    string BuildVersion { get; set; }
    string InstallDate { get; set; }
    string LastBootUpTime { get; set; }
}