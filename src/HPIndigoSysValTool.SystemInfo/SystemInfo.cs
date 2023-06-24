using System.Diagnostics;
using System.Runtime.InteropServices;
using HPIndigoSysValTool.Handlers.Logger;
using HPIndigoSysValTool.SystemInfo.Components;
using HPIndigoSysValTool.SystemInfo.Windows;

namespace HPIndigoSysValTool.SystemInfo;

public class SysInfo : ISysInfo
{
    private static readonly object _lock = new();
    private static SysInfo _systemInfo;
    private readonly ISystemInfoRetrieval _systemInfoRetrieval = null!;
    private List<IMemory> _memoryList = new();
    private List<INetworkCard> _networkCardList = new();
    private List<IProcessor> _processorList = new();
    private List<IStorage> _storageList = new();

    private List<ISystemInformation> _systemInformationList = new();
    private List<IVideo> _videoList = new();

    //public SysInfo()
    //{
    //    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    //    {
    //        _systemInfoRetrieval = new SystemInfoRetrieval();
    //        LoggerProvider.FileLogger.Information("Start Retrieving System and Hardware information for Windows");
    //        RefreshAll();
    //    }
    //}

    public SysInfo()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            _systemInfoRetrieval = new SystemInfoRetrieval();
            LoggerProvider.FileLogger.Information("Start Retrieving System and Hardware information for Windows");

            RefreshAll();
        }
    }

    public static SysInfo Instance
    {
        get
        {
            lock (_lock)
            {
                if (_systemInfo == null) _systemInfo = new SysInfo();
                return _systemInfo;
            }
        }
    }


    public bool IsLoaded { get; private set; }

    #region Properties

    public List<ISystemInformation> SystemInformationList
    {
        get
        {
            lock (_lock)
            {
                return new List<ISystemInformation>(_systemInformationList);
            }
        }
        private set
        {
            lock (_lock)
            {
                _systemInformationList = value;
            }
        }
    }

    public List<IProcessor> ProcessorList
    {
        get
        {
            lock (_lock)
            {
                return new List<IProcessor>(_processorList);
            }
        }
        private set
        {
            lock (_lock)
            {
                _processorList = value;
            }
        }
    }

    public List<IMemory> MemoryList
    {
        get
        {
            lock (_lock)
            {
                return new List<IMemory>(_memoryList);
            }
        }
        private set
        {
            lock (_lock)
            {
                _memoryList = value;
            }
        }
    }

    public List<IStorage> StorageList
    {
        get
        {
            lock (_lock)
            {
                return new List<IStorage>(_storageList);
            }
        }
        private set
        {
            lock (_lock)
            {
                _storageList = value;
            }
        }
    }

    public List<IVideo> VideoList
    {
        get
        {
            lock (_lock)
            {
                return new List<IVideo>(_videoList);
            }
        }
        private set
        {
            lock (_lock)
            {
                _videoList = value;
            }
        }
    }

    public List<INetworkCard> NetworkCardList
    {
        get
        {
            lock (_lock)
            {
                return new List<INetworkCard>(_networkCardList);
            }
        }
        private set
        {
            lock (_lock)
            {
                _networkCardList = value;
            }
        }
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Initializes a new instance of the <see cref="SystemInfo" /> class.
    /// </summary>
    //public void RefreshAll()
    //{
    //    RefreshSystemInformation();
    //    RefreshProcessor();
    //    RefreshMemory();
    //    RefreshStorage();
    //    RefreshVideo();
    //    RefreshNetworkCard();

    //    IsLoaded = true;
    //}


    public void RefreshAll()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        RefreshNetworkCard();
        stopwatch.Stop();
        LoggerProvider.FileLogger.Information($"Time taken to refresh network card information: {stopwatch.Elapsed.Seconds} s");

        RefreshSystemInformation();
        RefreshProcessor();
        RefreshMemory();
        RefreshStorage();
        RefreshVideo();



        IsLoaded = true;
    }


    public void RefreshSystemInformation()
    {
        SystemInformationList = _systemInfoRetrieval.GetSystemInformationList();
    }

    public void RefreshProcessor()
    {
        ProcessorList = _systemInfoRetrieval.GetProcessorList();
    }

    public void RefreshMemory()
    {
        MemoryList = _systemInfoRetrieval.GetMemoryList();
    }

    public void RefreshStorage()
    {
        StorageList = _systemInfoRetrieval.GetStorageList();
    }

    public void RefreshVideo()
    {
        VideoList = _systemInfoRetrieval.GetVideoList();
    }

    public void RefreshNetworkCard()
    {
        NetworkCardList = _systemInfoRetrieval.GetNetworkCardList();
    }

    #endregion


    #region Async Methods

    public async Task RefreshAllAsync()
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        await RefreshSystemInformationAsync();
        stopwatch.Stop();
        LoggerProvider.FileLogger.Information($"Time taken to refresh system information: {stopwatch.ElapsedMilliseconds} ms");

        stopwatch.Restart();
        await RefreshProcessorAsync();
        stopwatch.Stop();
        LoggerProvider.FileLogger.Information($"Time taken to refresh processor information: {stopwatch.ElapsedMilliseconds} ms");

        stopwatch.Restart();
        await RefreshMemoryAsync();
        stopwatch.Stop();
        LoggerProvider.FileLogger.Information($"Time taken to refresh memory information: {stopwatch.ElapsedMilliseconds} ms");

        stopwatch.Restart();
        await RefreshStorageAsync();
        stopwatch.Stop();
        LoggerProvider.FileLogger.Information($"Time taken to refresh storage information: {stopwatch.ElapsedMilliseconds} ms");

        stopwatch.Restart();
        await RefreshVideoAsync();
        stopwatch.Stop();
        LoggerProvider.FileLogger.Information($"Time taken to refresh video information: {stopwatch.ElapsedMilliseconds} ms");

        stopwatch.Restart();
        await RefreshNetworkCardAsync();
        stopwatch.Stop();
        LoggerProvider.FileLogger.Information($"Time taken to refresh network card information: {stopwatch.ElapsedMilliseconds} ms");

        IsLoaded = true;
    }

    // LoggerProvider.FileLogger.Information
    public async Task RefreshSystemInformationAsync()
    {
        var systemInformationTask = Task.Run(() => _systemInfoRetrieval.GetSystemInformationList());
        var systemInformation = await systemInformationTask;
        lock (_lock)
        {
            SystemInformationList = systemInformation;
        }
    }

    public async Task RefreshProcessorAsync()
    {
        var processorListTask = Task.Run(() => _systemInfoRetrieval.GetProcessorList());
        var processorList = await processorListTask;
        lock (_lock)
        {
            ProcessorList = processorList;
        }
    }

    public async Task RefreshMemoryAsync()
    {
        var memoryListTask = Task.Run(() => _systemInfoRetrieval.GetMemoryList());
        var memoryList = await memoryListTask;
        lock (_lock)
        {
            MemoryList = memoryList;
        }
    }

    public async Task RefreshStorageAsync()
    {
        var storageListTask = Task.Run(() => _systemInfoRetrieval.GetStorageList());
        var storageList = await storageListTask;
        lock (_lock)
        {
            StorageList = storageList;
        }
    }

    public async Task RefreshVideoAsync()
    {
        var videoListTask = Task.Run(() => _systemInfoRetrieval.GetVideoList());
        var videoList = await videoListTask;
        lock (_lock)
        {
            VideoList = videoList;
        }
    }

    public async Task RefreshNetworkCardAsync()
    {
        var networkCardListTask = Task.Run(() => _systemInfoRetrieval.GetNetworkCardList());
        var networkCardList = await networkCardListTask;
        lock (_lock)
        {
            NetworkCardList = networkCardList;
        }
    }

    #endregion
}