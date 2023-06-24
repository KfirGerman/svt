using System.Management;
using System.Runtime.Versioning;
using HPIndigoSysValTool.Handlers.Logger;
using HPIndigoSysValTool.SystemInfo.Components;
using HPIndigoSysValTool.SystemInfo.Helpers;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class VideoInformation
{
    private static readonly object _lock = new();

    public static List<IVideo> Get()
    {
        return GetVideoInformation();
    }

    private static List<IVideo> GetVideoInformation()
    {
        LoggerProvider.FileLogger.Information("Getting Video Information");

        List<Win32_VideoController>? win32_VideoController = null;
        lock (_lock)
        {
            try
            {
                win32_VideoController = Win32_VideoController.Get();
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error("Error getting Video Information");
#if DEBUG
                LoggerProvider.FileLogger.Error(ex.Message);
#endif
                throw;
            }
        }

        var videoCards = new List<IVideo>();
        var filteredvideoCards = win32_VideoController
            .Where(vc => vc.Caption.ToLower().Contains("nvidia"))
            .ToList();

        for (int i = 0; i < filteredvideoCards.Count; i++)
        {
            var video = filteredvideoCards[i];
            var pcieDetails = PCIeInformation.GetPCIeSlotData(video.PNPDeviceID, i, "nvlddmkm");
            videoCards.Add(new Video
            {
                AdapterCompatibility = video.AdapterCompatibility,
                Caption = video.Caption,
                PNPDeviceID = video.PNPDeviceID,
                DriverDate = ManagementDateTimeConverter.ToDateTime(video.DriverDate).ToString(),
                DriverVersion = video.DriverVersion,
                InstallDate = video.InstallDate,
                AdapterRAM = Converters.GetByteSizeString((ulong)video.AdapterRAM),
                SlotDesignation = pcieDetails.SlotDesignation,
                BusNumber = pcieDetails.BusNumber,
                DeviceNumber = pcieDetails.DeviceNumber,
                FunctionNumber = pcieDetails.FunctionNumber,

                //DeviceName = pcieDetails.DeviceName
            });
        }
        return videoCards;
    }
}