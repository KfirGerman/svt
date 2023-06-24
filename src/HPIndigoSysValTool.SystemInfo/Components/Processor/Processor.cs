using System.ComponentModel;

namespace HPIndigoSysValTool.SystemInfo.Components;

public class Processor : IProcessor
{
    [DisplayName("Caption")] public string? Caption { get; set; }
    [DisplayName("Name")] public string? Name { get; set; }

    [DisplayName("Device ID")] public string? DeviceID { get; set; }

    //[DisplayName("Description")] public string? Description { get; set; }

    [DisplayName("Socket Designation")] public string? SocketDesignation { get; set; }

    [DisplayName("Manufacturer")] public string? Manufacturer { get; set; }


    [DisplayName("Number of Cores")] public uint? NumberOfCores { get; set; }

    [DisplayName("Number of Logical Processors")] public uint? NumberOfLogicalProcessors { get; set; }

    //[DisplayName("Processor ID")] public string? ProcessorId { get; set; }
}