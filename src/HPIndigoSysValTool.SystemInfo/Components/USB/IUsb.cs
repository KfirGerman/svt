namespace HPIndigoSysValTool.SystemInfo.Components;

public interface IUsb
{
    string? Caption { get; set; }
    string? Description { get; set; }
    string? DeviceID { get; set; }
    string? Manufacturer { get; set; }
    string? Name { get; set; }
    string? PNPDeviceID { get; set; }
    string? Status { get; set; }
    string? Service { get; set; }
    string? USBVersion { get; set; }
    string? VendorID { get; set; }
    string? ProductID { get; set; }
}