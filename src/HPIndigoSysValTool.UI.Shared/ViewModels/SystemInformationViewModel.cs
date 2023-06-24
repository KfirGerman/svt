using System.Collections.ObjectModel;
using HPIndigoSysValTool.SystemInfo;
using HPIndigoSysValTool.SystemInfo.Components;

namespace HPIndigoSysValTool.UI.Shared.ViewModels
{
    public class SystemInformationViewModel : BaseViewModel
    {
        private readonly SysInfo _systemInfo;

        public ObservableCollection<ISystemInformation> SystemInformationList { get; set; }
        public ObservableCollection<IProcessor> ProcessorList { get; set; }
        public ObservableCollection<IMemory> MemoryList { get; set; }
        public ObservableCollection<IStorage> StorageList { get; set; }
        public ObservableCollection<IVideo> VideoList { get; set; }
        public ObservableCollection<INetworkCard> NetworkCardList { get; set; }

        public SystemInformationViewModel()
        {
            _systemInfo = SysInfo.Instance;
            SystemInformationList = new ObservableCollection<ISystemInformation>(_systemInfo.SystemInformationList);
            ProcessorList = new ObservableCollection<IProcessor>(_systemInfo.ProcessorList);
            MemoryList = new ObservableCollection<IMemory>(_systemInfo.MemoryList);
            StorageList = new ObservableCollection<IStorage>(_systemInfo.StorageList);
            VideoList = new ObservableCollection<IVideo>(_systemInfo.VideoList);
            NetworkCardList = new ObservableCollection<INetworkCard>(_systemInfo.NetworkCardList);
        }
    }
}
