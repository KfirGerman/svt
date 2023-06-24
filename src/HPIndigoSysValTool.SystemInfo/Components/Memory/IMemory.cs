namespace HPIndigoSysValTool.SystemInfo.Components;

/// <summary>
///     from Win32_PhysicalMemory
/// </summary>
public interface IMemory
{
    string BankLabel { get; set; }
    string DeviceLocator { get; set; }
    string Manufacturer { get; set; }
    string SerialNumber { get; set; }
    string PartNumber { get; set; }
    string Capacity { get; set; }
    string Speed { get; set; }
    string SMBIOSMemoryType { get; set; }
    //string TypeDetail { get; set; }
    //string ToString();
}