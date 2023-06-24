using HPIndigoSysValTool.SystemInfo.Components;

namespace HPIndigoSysValTool.SystemInfo;

public interface ISysInfo
{
    #region Properties

    List<ISystemInformation> SystemInformationList { get; }

    List<IProcessor> ProcessorList { get; }

    List<IMemory> MemoryList { get; }

    List<IStorage> StorageList { get; }
    List<IVideo> VideoList { get; }

    List<INetworkCard> NetworkCardList { get; }
    bool IsLoaded { get; }

    #endregion

    #region Regular Methods

    void RefreshAll();
    void RefreshSystemInformation();
    void RefreshProcessor();
    void RefreshMemory();
    void RefreshStorage();
    void RefreshVideo();
    void RefreshNetworkCard();

    #endregion

    #region Async Methods

    Task RefreshAllAsync();
    Task RefreshSystemInformationAsync();
    Task RefreshProcessorAsync();
    Task RefreshMemoryAsync();
    Task RefreshStorageAsync();
    Task RefreshVideoAsync();
    Task RefreshNetworkCardAsync();

    #endregion
}