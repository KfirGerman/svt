using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using HPIndigoSysValTool.Handlers.Logger;
using HPIndigoSysValTool.SystemInfo.Components;

namespace HPIndigoSysValTool.SystemInfo.Windows;


[SupportedOSPlatform("windows")]


/// <summary>
/// Class to get PCIe information for a given device ID.
/// </summary>
public class PCIeInformation
{

    private static Lazy<IEnumerable<Win32_PnPEntity_Instance>> _win32_PnPEntity = new Lazy<IEnumerable<Win32_PnPEntity_Instance>>(Win32_PnPEntity.GetInstances);
    private static readonly Lazy<IEnumerable<Win32_PnPSignedDriver>> _win32_PnPSignedDriver =
        new Lazy<IEnumerable<Win32_PnPSignedDriver>>(() => Win32_PnPSignedDriver.Get());

    private static readonly Lazy<IEnumerable<Win32_SystemSlot>> _win32_SystemSlot =
        new Lazy<IEnumerable<Win32_SystemSlot>>(() => Win32_SystemSlot.Get());


    /// <summary>
    ///     Get the PCIe slot information for a given device ID.
    /// </summary>
    /// <param name="hardwareId">The device ID to search for.</param>
    /// <param name="args">Possible additional arguments, such as device index.</param>
    /// <returns>A list of PCIe objects containing the device name, ID, slot designation, and bus number.</returns>
    public static IPCIe? GetPCIeSlotData(string hardwareId, int index = 0, string? serviceName = null)
    {
        if (string.IsNullOrEmpty(hardwareId)) return null;

        LoggerProvider.FileLogger.Information($"Getting PCIe Information for {hardwareId}");

        var matchingEntity = _win32_PnPEntity.Value.FirstOrDefault(x => x.DeviceID.Contains(hardwareId) && (string.IsNullOrEmpty(serviceName) || x.Service == serviceName));

        if (matchingEntity == null)
        {
            LoggerProvider.FileLogger.Error("Error getting PCIe Information");
            return null;
        }


        var deviceLocation = GetDeviceLocation(hardwareId);
        var devicePCIeLocation = GetPCIeDeviceLocation(deviceLocation);

        if (devicePCIeLocation != null)
        {
            var busNum = devicePCIeLocation["PCI bus"];
            var deviceNum = devicePCIeLocation["device"];
            var functionNum = devicePCIeLocation["function"];
            var slotDesignation = GetSlotDesignation(busNum);
            var deviceName = matchingEntity.Caption;
            return new PCIe
            {
                //DeviceName = deviceName,
                //DeviceID = matchingEntity.DeviceID,
                BusNumber = busNum,
                SlotDesignation = slotDesignation,
                DeviceNumber = deviceNum,
                FunctionNumber = functionNum
            };
        }

        LoggerProvider.FileLogger.Error("Error getting PCIe Device information");
        return null;
    }
    /// <summary>
    ///     Get location string for each device.
    /// </summary>
    /// <param name="deviceId"></param>
    /// <returns>Location</returns>
    private static string GetDeviceLocation(string deviceId)
    {
        if (string.IsNullOrEmpty(deviceId))
        {
            LoggerProvider.FileLogger.Error("Error getting device location: DeviceID is null or empty");
            return "N/A";
        }

        try
        {
            var device = _win32_PnPSignedDriver.Value.FirstOrDefault(x => x.DeviceID != null && x.DeviceID.Contains(deviceId));

            if (device != null)
            {
                return device.Location ?? "N/A";
            }
            else
            {
                LoggerProvider.FileLogger.Error($"Device with ID: {deviceId} not found");
                return "N/A";
            }
        }
        catch (Exception ex)
        {
            LoggerProvider.FileLogger.Error($"Error getting device location for device with ID: {deviceId}");
#if DEBUG
            LoggerProvider.FileLogger.Error(ex.Message);
#endif
            return "N/A";
        }
    }


    /// <summary>
    ///     Get the PCIe device location information from the system.
    /// </summary>
    /// <param name="deviceLocation"></param>
    /// <returns>Dictionary: PCI bus, device, function</returns>
    private static IDictionary<string, int?>? GetPCIeDeviceLocation(string deviceLocation)
    {
        try
        {
            var locationInfo = _locationRegex.Match(deviceLocation).Groups;

            if (locationInfo.Count > 0)
            {
                int.TryParse(locationInfo["busNum"].Value, out var busNumber);
                int.TryParse(locationInfo["deviceNum"].Value, out var deviceNumber);
                int.TryParse(locationInfo["functionNum"].Value, out var functionNumber);

                return new Dictionary<string, int?>
                {
                    ["PCI bus"] = busNumber,
                    ["device"] = deviceNumber,
                    ["function"] = functionNumber
                };
            }
            else
            {
                return new Dictionary<string, int?>
                {
                    ["PCI bus"] = null,
                    ["device"] = null,
                    ["function"] = null
                };
            }
        }
        catch (Exception ex)
        {
            LoggerProvider.FileLogger.Error("Error getting PCIe device location");
#if DEBUG
            LoggerProvider.FileLogger.Error(ex.Message);
#endif
            return null;
        }
    }

    /// <summary>
    ///     Get the slot designation for a given bus number.
    /// </summary>
    /// <param name="busNumber"></param>
    /// <returns></returns>
    private static string GetSlotDesignation(int? busNumber)
    {
        // check if busNumber is not null 
        if (busNumber == null)
        {
            LoggerProvider.FileLogger.Error(
                "Error getting slot designation for a given bus number: Null or Empty value provided");
            return "N/A";
        }

        try
        {
            var slot = _win32_SystemSlot.Value.FirstOrDefault(x => x.BusNumber == busNumber);
            return slot?.SlotDesignation ?? "N/A";
        }
        catch (Exception ex)
        {
            LoggerProvider.FileLogger.Error($"Error getting slot designation for a given bus number: {busNumber}");
#if DEBUG
            LoggerProvider.FileLogger.Error(ex.Message);
#endif

            return "N/A";
        }
    }

    #region Private Fields

    private static readonly Regex _locationRegex =
        new(@"(?<bus>PCI bus (?<busNum>[0-9]+), device (?<deviceNum>[0-9]+), function (?<functionNum>[0-9]+))",
            RegexOptions.Compiled);

    #endregion
}