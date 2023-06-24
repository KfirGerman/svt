namespace HPIndigoSysValTool.SystemInfo.Components;

public interface IVideo : IPCIe
{
    string? AdapterCompatibility { get; set; }
    string? Caption { get; set; }
    string? PNPDeviceID { get; set; }
    string? DriverDate { get; set; }
    string? DriverVersion { get; set; }
    string? InstallDate { get; set; }
    string? AdapterRAM { get; set; }
}