using HPIndigoSysValTool.SystemInfo.Components;

namespace HPIndigoSysValTool.SystemInfo.Windows;

public class SystemInfoRetrieval : ISystemInfoRetrieval
{
    #region Methods

    public List<ISystemInformation> GetSystemInformationList()
    {
        return SystemInformation.Get();
    }

    public List<IProcessor> GetProcessorList()
    {
        return ProcessorInformation.Get();
    }

    public List<IStorage> GetStorageList()
    {
        return StorageInformation.Get();
    }

    public List<IMemory> GetMemoryList()
    {
        return MemoryInformation.Get();
    }

    public List<IVideo> GetVideoList()
    {
        return VideoInformation.Get();
    }

    public List<INetworkCard> GetNetworkCardList()
    {
        return NetworkCardInformation.Get();
    }

    #endregion
}