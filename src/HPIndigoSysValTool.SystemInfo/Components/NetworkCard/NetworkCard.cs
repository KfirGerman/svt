using System.ComponentModel;

namespace HPIndigoSysValTool.SystemInfo.Components
{
    public class NetworkCard : INetworkCard
    {
        [DisplayName("Name")] public string? Name { get; set; }
        [DisplayName("Manufacturer")] public string? Manufacturer { get; set; }
        //[DisplayName("Availability")] public uint? Availability { get; set; }
        //[DisplayName("Device Name")] public string? DeviceName { get; set; }
        //[DisplayName("Device ID")] public string? DeviceID { get; set; }
        //[DisplayName("Vendor ID")] public string? VendorID { get; set; }
        [DisplayName("Slot Designation")] public string? SlotDesignation { get; set; }
        [DisplayName("Bus Number")] public int? BusNumber { get; set; }
        [DisplayName("Device Number")] public int? DeviceNumber { get; set; }
        [DisplayName("Function Number")] public int? FunctionNumber { get; set; }
        //[DisplayName("Index")] public uint? Index { get; set; }
        //[DisplayName("Install Date")] public string? InstallDate { get; set; }
        //[DisplayName("Installed")] public bool? Installed { get; set; }
        //[DisplayName("Interface Index")] public uint? InterfaceIndex { get; set; }
        [DisplayName("MAC Address")] public string? MACAddress { get; set; }
        //[DisplayName("Max Number Controlled")] public uint? MaxNumberControlled { get; set; }
        [DisplayName("Net Connection ID")] public string? NetConnectionID { get; set; }
        //[DisplayName("Net Connection Status")] public uint? NetConnectionStatus { get; set; }
        [DisplayName("Net Enabled")] public bool? NetEnabled { get; set; }
        [DisplayName("Physical Adapter")] public bool? PhysicalAdapter { get; set; }
        [DisplayName("PNP Device ID")] public string? PNPDeviceID { get; set; }
        [DisplayName("Time of Last Reset")] public string? TimeOfLastReset { get; set; }
        [DisplayName("Default IP Gateway")] public string? DefaultIPGateway { get; set; }
        [DisplayName("DHCP Server")] public string? DHCPServer { get; set; }
        [DisplayName("DHCP Enabled")] public bool? DHCPEnabled { get; set; }
        [DisplayName("DNS Server Search Order")] public string? DNSServerSearchOrder { get; set; }
        [DisplayName("IP Address")] public string? IPAddress { get; set; }
        [DisplayName("IP Subnet")] public string? IPSubnet { get; set; }
        //[DisplayName("Setting ID")] public string? SettingID { get; set; }
    }
}
