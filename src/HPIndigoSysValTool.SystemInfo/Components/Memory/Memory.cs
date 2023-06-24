using System.ComponentModel;

namespace HPIndigoSysValTool.SystemInfo.Components;

[Flags]
public enum SMBIOSMemoryType
{
    Other = 1,
    Unknown = 2,
    DDR3 = 24,
    DDR4 = 26
}

[Flags]
public enum TypeDetail
{
    Reserved = 1,
    Other = 2,
    Unknown = 4,
    FastPaged = 8,
    StaticColumn = 16,
    PseudoStatic = 32,
    RAMBUS = 64,
    Synchronous = 128,
    CMOS = 256,
    EDO = 512,
    WindowDRAM = 1024,
    CacheDRAM = 2048,
    NonVolatile = 4096
}

public class Memory : IMemory
{
    [DisplayName("Bank Label")] public string BankLabel { get; set; }

    [DisplayName("Device Locator")] public string DeviceLocator { get; set; }

    [DisplayName("Manufacturer")] public string Manufacturer { get; set; }

    [DisplayName("Part Number")] public string PartNumber { get; set; }

    [DisplayName("Serial Number")] public string SerialNumber { get; set; }

    [DisplayName("Capacity")] public string Capacity { get; set; }

    [DisplayName("Speed")] public string Speed { get; set; }

    [DisplayName("SMBIOS Memory Type")] public string SMBIOSMemoryType { get; set; }

    //[DisplayName("Type Detail")] public string TypeDetail { get; set; }
}