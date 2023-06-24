#nullable enable
using System.Management;
using System.Runtime.CompilerServices;

namespace HPIndigoSysValTool.SystemInfo.Windows;

public class Win32_PnPEntity_Instance
{
    public Win32_PnPEntity_Instance(ManagementObject mo)
    {
        if (mo == null)
            return;
        instance = mo;
        Caption = ToString(mo[nameof(Caption)]);
        Description = ToString(mo[nameof(Description)]);
        Name = ToString(mo[nameof(Name)]);
        DeviceID = ToString(mo[nameof(DeviceID)]);
        PNPClass = ToString(mo[nameof(PNPClass)]);
        Status = ToString(mo[nameof(Status)]);
        Manufacturer = ToString(mo[nameof(Manufacturer)]);
        Service = ToString(mo[nameof(Service)]);
        bool result1;
        bool.TryParse(ToString(mo[nameof(Present)]), out result1);
        Present = result1;
        int result2;
        int.TryParse(ToString(mo[nameof(ConfigManagerErrorCode)]), out result2);
        ConfigManagerErrorCode = result2;
    }

    private string ToString(object mo)
    {
        return mo != null ? mo.ToString() ?? string.Empty : string.Empty;
    }

    public bool Enabled()
    {
        Refresh();
        return ConfigManagerErrorCode != 22;
    }

    public Result Disable()
    {
        var result = new Result();
        if (instance == null)
            return result;
        var managementBaseObject = instance.InvokeMethod(
            nameof(Disable),
            instance.GetMethodParameters(nameof(Disable)),
            null);
        uint.TryParse(managementBaseObject["ReturnValue"].ToString(), out result.ReturnValue);
        bool.TryParse(managementBaseObject["rebootNeeded"].ToString(), out result.RebootNeeded);
        return result;
    }


    public Result Enable()
    {
        var result = new Result();
        if (instance == null)
            return result;
        var managementBaseObject =
            instance.InvokeMethod(nameof(Enable), instance.GetMethodParameters(nameof(Enable)), null);
        uint.TryParse(managementBaseObject["ReturnValue"].ToString(), out result.ReturnValue);
        bool.TryParse(managementBaseObject["rebootNeeded"].ToString(), out result.RebootNeeded);
        return result;
    }

    public Result Reset()
    {
        var result = new Result();
        if (instance == null)
            return result;
        uint.TryParse(
            instance.InvokeMethod(nameof(Reset), instance.GetMethodParameters(nameof(Reset)), null)["ReturnValue"]
                .ToString(), out result.ReturnValue);
        return result;
    }

    public ManagementBaseObject? SetPowerState(uint powerState, DateTime dateTime)
    {
        return instance?.InvokeMethod(nameof(SetPowerState), instance?.GetMethodParameters(nameof(SetPowerState)),
            null) ?? null;
    }


    public ManagementBaseObject? GetDeviceProperties(string[] propertyKeys)
    {
        return instance?.InvokeMethod(nameof(GetDeviceProperties),
            instance?.GetMethodParameters(nameof(GetDeviceProperties)), null) ?? null;
    }

    /// <summary>
    ///     Get the error message for the ConfigManagerErrorCode
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public string GetConfigManagerErrorMessage(int code)
    {
        if (code >= 0 && code <= 31)
            return ConfigManagerErrorCodes[code];
        var interpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
        interpolatedStringHandler.AppendLiteral("Error ");
        interpolatedStringHandler.AppendFormatted(code);
        return interpolatedStringHandler.ToStringAndClear();
    }

    /// <summary>
    ///     Refresh the instance
    /// </summary>
    public void Refresh()
    {
        var pnPentityInstance = Win32_PnPEntity.GetInstancesByID(DeviceID).FirstOrDefault();
        if (pnPentityInstance == null)
            return;
        Caption = pnPentityInstance.Caption;
        Description = pnPentityInstance.Description;
        Name = pnPentityInstance.Name;
        DeviceID = pnPentityInstance.DeviceID;
        PNPClass = pnPentityInstance.PNPClass;
        Status = pnPentityInstance.Status;
        Manufacturer = pnPentityInstance.Manufacturer;
        Service = pnPentityInstance.Service;
        Present = pnPentityInstance.Present;
        ConfigManagerErrorCode = pnPentityInstance.ConfigManagerErrorCode;
    }

    /// <summary>
    ///     Get the instance of the device
    /// </summary>
    public class Result
    {
        public bool RebootNeeded;
        public uint ReturnValue;
    }

    #region Properties

    private readonly ManagementObject? instance;

    private readonly string[] ConfigManagerErrorCodes = new string[32]
    {
        "This device is working properly.",
        "This device is not configured correctly.",
        "Windows cannot load the driver for this device.",
        "The driver for this device might be corrupted, or your system may be running low on memory or other resources.",
        "This device is not working properly. One of its drivers or your registry might be corrupted.",
        "The driver for this device needs a resource that Windows cannot manage.",
        "The boot configuration for this device conflicts with other devices.",
        "Cannot filter.",
        "The driver loader for the device is missing.",
        "This device is not working properly because the controlling firmware is reporting the resources for the device incorrectly.",
        "This device cannot start.",
        "This device failed.",
        "This device cannot find enough free resources that it can use.",
        "Windows cannot verify this device's resources.",
        "This device cannot work properly until you restart your computer.",
        "This device is not working properly because there is probably a re-enumeration problem.",
        "Windows cannot identify all the resources this device uses.",
        "This device is asking for an unknown resource type.",
        "Reinstall the drivers for this device.",
        "Your registry might be corrupted.",
        "Failure using the VxD loader.",
        "System failure: Try changing the driver for this device.If that does not work, see your hardware documentation. Windows is removing this device.",
        "This device is disabled.",
        "System failure: Try changing the driver for this device.If that doesn't work, see your hardware documentation.",
        "This device is not present, is not working properly, or does not have all its drivers installed.",
        "Windows is still setting up this device.",
        "Windows is still setting up this device.",
        "This device does not have valid log configuration.",
        "The drivers for this device are not installed.",
        "This device is disabled because the firmware of the device did not give it the required resources.",
        "This device is using an Interrupt Request(IRQ) resource that another device is using.",
        "This device is not working properly because Windows cannot load the drivers required for this device."
    };

    public string DeviceID { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Caption { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string PNPClass { get; set; } = string.Empty;

    public bool Present { get; set; }

    public string Status { get; set; } = string.Empty;

    public string Manufacturer { get; set; } = string.Empty;

    public string Service { get; set; } = string.Empty;

    public int ConfigManagerErrorCode { get; set; }

    #endregion Properties
}