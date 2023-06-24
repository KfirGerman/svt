namespace HPIndigoSysValTool.SystemInfo.Components;

/// <summary>
///     WMI class: Win32_Processor
/// </summary>
public interface IProcessor
{
    /// <summary>
    ///     Short description of an object (a one-line string).
    /// </summary>
    string? Caption { get; set; }

    /// <summary>
    ///     Label by which the object is known.
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    ///     Gets the processor type.
    /// </summary>
    string? DeviceID { get; set; }

    /// <summary>
    ///     Description of the object.
    /// </summary>
    // string? Description { get; set; }

    /// <summary>
    ///     Type of chip socket used on the circuit.
    /// </summary>
    string? SocketDesignation { get; set; }

    /// <summary>
    ///     Name of the processor manufacturer.
    /// </summary>
    string? Manufacturer { get; set; }


    /// <summary>
    ///     Number of cores for the current instance of the processor. A core is a physical processor on the integrated
    ///     circuit. For example, in a dual-core processor this property has a value of 2.
    /// </summary>
    uint? NumberOfCores { get; set; }

    /// <summary>
    ///     Number of logical processors for the current instance of the processor. For processors capable of hyperthreading,
    ///     this value includes only the processors which have hyperthreading enabled.
    /// </summary>
    uint? NumberOfLogicalProcessors { get; set; }

    /// <summary>
    ///     Processor information that describes the processor features.
    ///     For an x86 class CPU, the field format depends on the processor support of the CPUID instruction.
    ///     If the instruction is supported, the property contains 2 (two) DWORD formatted values.
    ///     The first is an offset of 08h-0Bh, which is the EAX value that a CPUID instruction returns with input EAX set to 1.
    ///     The second is an offset of 0Ch-0Fh, which is the EDX value that the instruction returns.
    ///     Only the first two bytes of the property are significant and contain the contents of the DX register at CPU
    ///     reset—all others are set to 0 (zero), and the contents are in DWORD format.
    /// </summary>
    //string? ProcessorId { get; set; }
}