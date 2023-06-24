using HPIndigoSysValTool.SystemInfo.Components;

namespace HPIndigoSysValTool.SystemInfo;

public interface ISystemInfoRetrieval
{
    // Regular properties
    List<ISystemInformation> GetSystemInformationList();
    List<IProcessor> GetProcessorList();
    List<IStorage> GetStorageList();
    List<IMemory> GetMemoryList();
    List<IVideo> GetVideoList();
    List<INetworkCard> GetNetworkCardList();
}