namespace HPIndigoSysValTool.SystemInfo.Components;

/// <summary>
///     This class is used to get the PCIe information from the system.
/// </summary>
public interface IPCIe
{
    //string? DeviceName { get; set; }
    string? SlotDesignation { get; set; }
    int? BusNumber { get; set; }
    int? DeviceNumber { get; set; }
    int? FunctionNumber { get; set; }
}