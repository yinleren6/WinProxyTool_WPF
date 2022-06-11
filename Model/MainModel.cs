using System.ComponentModel;
using System.Windows.Input;
using WinProxyTool_WPF.ViewModel;

namespace WinProxyTool_WPF.Model
{
    public class MainModel : ViewModelBase
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _proxyEnable;
        private string? _proxyIp;
        private string? _inputProxyIp;
        private string? _proxyPort;
        private string? _inputProxyPort;
        private string? _proxyOverride;
        private string? _toggleText;
        public string ToggleText
        {
            get => _toggleText;
            set => SetProperty(ref _toggleText, value);
        }
        public int ProxyEnable
        {
            get => _proxyEnable;
            set => SetProperty(ref _proxyEnable, value);
        }
        public string ProxyIP
        {
            get => _proxyIp;
            set => SetProperty(ref _proxyIp, value);
        }
        public string ProxyPort
        {
            get => _proxyPort;
            set => SetProperty(ref _proxyPort, value);
        }
        public string ProxyOverride
        {
            get => _proxyOverride;
            set => SetProperty(ref _proxyOverride, value);
            //set
            //{
            //    _proxyOverride = value;
            //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProxyOverride)));

            //}
        }
        public string InputProxyIP
        {
            get => _inputProxyIp;
            set => SetProperty(ref _inputProxyIp, value);
        }
        public string InputProxyPort
        {
            get => _inputProxyPort;
            set => SetProperty(ref _inputProxyPort, value);
        }
        //开关
        public ICommand? ToggleProxy { get; set; }
        public ICommand? Sync { get; set; }
        //保存
        public ICommand? SaveConfig { get; set; }
        //更新数据
        public ICommand? UpdateStat { get; set; }

    }
}
