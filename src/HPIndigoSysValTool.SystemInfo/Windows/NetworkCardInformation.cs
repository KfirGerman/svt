using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using HPIndigoSysValTool.Handlers.Logger;
using HPIndigoSysValTool.SystemInfo.Components;
using HPIndigoSysValTool.SystemInfo.Helpers;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class NetworkCardInformation
{
    private static readonly object _lock = new();

    public static List<INetworkCard> Get()
    {
        return GetNetworkCardInformation();
    }

    /// <summary>
    ///     Get the network card information
    /// </summary>
    /// <returns></returns>
    private static List<INetworkCard> GetNetworkCardInformation()
    {
        LoggerProvider.FileLogger.Information("Getting Network Card Information");

        List<Win32_NetworkAdapter>? win32_NetworkAdapter = null;
        List<Win32_NetworkAdapterConfiguration>? win32_NetworkAdapterConfiguration = null;

        lock (_lock)
        {
            try
            {
                win32_NetworkAdapter = Win32_NetworkAdapter.Get();
                win32_NetworkAdapterConfiguration = Win32_NetworkAdapterConfiguration.Get();
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error("Error getting Network Card Information");
#if DEBUG
                LoggerProvider.FileLogger.Error(ex.Message);
#endif
                throw;
            }
        }

        var networkCards = new List<INetworkCard>();

        // Filter the network adapters
        var filteredNetworkAdapters = win32_NetworkAdapter
            .Where(na => na.Manufacturer.ToLower().Contains("intel") || na.Manufacturer.Contains("hp"))
            .ToList();

        for (int i = 0; i < filteredNetworkAdapters.Count; i++)
        {
            var network = filteredNetworkAdapters[i];
            // check if network.ServiceName is not empty
            IPCIe? pcieDetails = null;
            if (!string.IsNullOrEmpty(network.ServiceName))
            {
                pcieDetails = PCIeInformation.GetPCIeSlotData(network.PNPDeviceID, i, network.ServiceName);
            }

            //var pcieDetails = PCIeInformation.GetPCIeSlotData(network.PNPDeviceID, i, network.ServiceName).FirstOrDefault();
            var networkDetails = win32_NetworkAdapterConfiguration.FirstOrDefault(nac => nac.SettingID.Contains(network.GUID));
            networkCards.Add(new NetworkCard
            {
                //Availability = network.Availability,
                //Caption = network.Caption,
                //Description = network.Description,
                //DeviceID = network.DeviceID,
                //GUID = network.GUID,
                //Index = network.Index,
                //InstallDate = Converters.ConvertDateTimeString(network.InstallDate),
                //Installed = network.Installed,
                //InterfaceIndex = network.InterfaceIndex,
                MACAddress = network.MACAddress,
                Manufacturer = network.Manufacturer,
                //MaxNumberControlled = network.MaxNumberControlled,
                Name = network.Name,
                NetConnectionID = network.NetConnectionID,
                //NetConnectionStatus = network.NetConnectionStatus,
                NetEnabled = network.NetEnabled,
                PhysicalAdapter = network.PhysicalAdapter,
                PNPDeviceID = network.PNPDeviceID,
                TimeOfLastReset = Converters.ConvertDateTimeString(network.TimeOfLastReset),
                DefaultIPGateway = GetIPv4Address(networkDetails.DefaultIPGateway),
                DHCPServer = GetIPv4Address(networkDetails.DHCPServer),
                DHCPEnabled = networkDetails.DHCPEnabled,
                DNSServerSearchOrder = GetIPv4Address(networkDetails.DNSServerSearchOrder),
                IPAddress = GetIPv4Address(networkDetails.IPAddress),
                IPSubnet = GetIPv4Address(networkDetails.IPSubnet),
                //SettingID = networkDetails.SettingID,
                SlotDesignation = pcieDetails?.SlotDesignation ?? "N/A",
                BusNumber = pcieDetails?.BusNumber ?? 0,
                //DeviceName = pcieDetails?.DeviceName ?? "N/A",
                DeviceNumber = pcieDetails?.DeviceNumber ?? 0,
                FunctionNumber = pcieDetails?.FunctionNumber ?? 0
                });
            }
        return networkCards;
    }
    private static string GetIPv4Address(string ipAddress)
    {
        var pattern = @"(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})";
        var regex = new Regex(pattern);
        var match = regex.Match(ipAddress);
        return match.Value;
    }
}