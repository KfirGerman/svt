using System.ComponentModel;

namespace HPIndigoSysValTool.SystemInfo.Components;

public class SystemInformation : ISystemInformation
{
    [DisplayName("Name")] public string Name { get; set; }

    [DisplayName("Manufacturer")] public string Manufacturer { get; set; }

    [DisplayName("Model")] public string Model { get; set; }

    [DisplayName("System SKU Number")] public string SystemSKUNumber { get; set; }

    [DisplayName("Memory Size")] public string MemorySize { get; set; }

    [DisplayName("BIOS Version")] public string BIOSVersion { get; set; }

    [DisplayName("BIOS Release Date")] public string BIOSReleaseDate { get; set; }

    [DisplayName("Installed OS Name")] public string InstalledOSName { get; set; }

    [DisplayName("Build Version")] public string BuildVersion { get; set; }

    [DisplayName("Install Date")] public string InstallDate { get; set; }

    [DisplayName("Last Boot Up Time")] public string LastBootUpTime { get; set; }
}