using System.Windows.Input;
using WinProxyTool_WPF.ViewModel;

namespace WinProxyTool_WPF.Model
{
    public class MainModel : ViewModelBase
    {
        // public event PropertyChangedEventHandler? PropertyChanged;

        private int? _proxyEnable;
        private string? _proxyIp;
        private string? _inputProxyIp;

        private int? _proxyPort;
        private int? _inputProxyPort;

        private string? _proxyOverride;
        private string? _inputProxyOverride;

        private bool _isSkipLocal;
        private bool _skipStatu;

        public int? ProxyEnable
        {
            get => _proxyEnable;
            set => SetProperty(ref _proxyEnable, value);
        }
        public string? ProxyIP
        {
            get => _proxyIp;
            set => SetProperty(ref _proxyIp, value);
        }
        public int? ProxyPort
        {
            get => _proxyPort;
            set => SetProperty(ref _proxyPort, value);
        }
        public string? ProxyOverride
        {
            get => _proxyOverride;
            set => SetProperty(ref _proxyOverride, value);
        }
        public bool IsSkipLocal
        {
            get => _isSkipLocal;
            set => SetProperty(ref _isSkipLocal, value);
        }
        public bool SkipStatu
        {
            get => _skipStatu;
            set => SetProperty(ref _skipStatu, value);
        }
        public string? InputProxyIP
        {
            get => _inputProxyIp;
            set => SetProperty(ref _inputProxyIp, value);
        }
        public int? InputProxyPort
        {
            get => _inputProxyPort;
            set => SetProperty(ref _inputProxyPort, value);
        }
        public string? InputProxyOverride
        {
            get => _inputProxyOverride;
            set => SetProperty(ref _inputProxyOverride, value);
        }
        //开关
        public ICommand? ToggleProxy { get; set; }
        //更新数据 
        public ICommand? UpdataProxyStatu { get; set; }
        //保存代理
        public ICommand? SaveProxyServer { get; set; }
        //同步对话框
        public ICommand? Sync { get; set; }
        public ICommand? ToggleOverride { get; set; }
        //更新数据 
        public ICommand? UpdataOverrideStatu { get; set; }
        //保存override
        public ICommand? SaveOverride { get; set; }
    }
}
