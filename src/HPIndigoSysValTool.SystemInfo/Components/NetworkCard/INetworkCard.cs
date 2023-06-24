namespace HPIndigoSysValTool.SystemInfo.Components
{
    public interface INetworkCard : IPCIe
    {
        string? Name { get; set; }

        string? Manufacturer { get; set; }

        //uint? Availability { get; set; }

        //uint? Index { get; set; }

        //string? InstallDate { get; set; }

        //bool? Installed { get; set; }

        //uint? InterfaceIndex { get; set; }

        string? MACAddress { get; set; }

        //uint? MaxNumberControlled { get; set; }

        string? NetConnectionID { get; set; }

        //uint? NetConnectionStatus { get; set; }

        bool? NetEnabled { get; set; }

        bool? PhysicalAdapter { get; set; }

        string? PNPDeviceID { get; set; }

        string? TimeOfLastReset { get; set; }

        string? DefaultIPGateway { get; set; }

        string? DHCPServer { get; set; }

        bool? DHCPEnabled { get; set; }

        string? DNSServerSearchOrder { get; set; }

        string? IPAddress { get; set; }

        string? IPSubnet { get; set; }

        //string? SettingID { get; set; }
    }
}