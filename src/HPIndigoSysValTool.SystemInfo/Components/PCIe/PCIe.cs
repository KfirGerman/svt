namespace HPIndigoSysValTool.SystemInfo.Components;

public class PCIe : IPCIe
{
    public string? SlotDesignation { get; set; }
    public int? BusNumber { get; set; }
    public int? DeviceNumber { get; set; }
    public int? FunctionNumber { get; set; }
}