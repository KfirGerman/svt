namespace HPIndigoSysValTool.SystemInfo.Components;

public class Usb : IUsb, IPCIe
{
    public string? DeviceName { get; set; }
    public string? SlotDesignation { get; set; }
    public int? BusNumber { get; set; }
    public int? DeviceNumber { get; set; }
    public int? FunctionNumber { get; set; }
    public string? Caption { get; set; }
    public string? Description { get; set; }
    public string? DeviceID { get; set; }
    public string? Manufacturer { get; set; }
    public string? Name { get; set; }
    public string? PNPDeviceID { get; set; }
    public string? Status { get; set; }
    public string? Service { get; set; }
    public string? USBVersion { get; set; }
    public string? VendorID { get; set; }
    public string? ProductID { get; set; }
}