using System.ComponentModel;

namespace HPIndigoSysValTool.SystemInfo.Components;

public class Video : IVideo
{
    [DisplayName("Adapter Compatibility")] public string? AdapterCompatibility { get; set; }

    [DisplayName("Caption")] public string? Caption { get; set; }

    //[DisplayName("Device Name")] public string? DeviceName { get; set; }
    //[DisplayName("Device ID")] public string? DeviceID { get; set; }
    //[DisplayName("Vendor ID")] public string? VendorID { get; set; }
    [DisplayName("Slot Designation")] public string? SlotDesignation { get; set; }
    [DisplayName("Bus Number")] public int? BusNumber { get; set; }
    [DisplayName("Device Number")] public int? DeviceNumber { get; set; }
    [DisplayName("Function Number")] public int? FunctionNumber { get; set; }
    [DisplayName("PNP Device ID")] public string? PNPDeviceID { get; set; }
    [DisplayName("Driver Date")] public string? DriverDate { get; set; }
    [DisplayName("Driver Version")] public string? DriverVersion { get; set; }
    [DisplayName("Install Date")] public string? InstallDate { get; set; }
    [DisplayName("Adapter RAM")] public string? AdapterRAM { get; set; }
}