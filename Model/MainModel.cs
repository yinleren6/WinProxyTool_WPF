using System.ComponentModel;
using System.Windows.Input;

namespace WinProxyTool_WPF.Model
{
    public class MainModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _proxyEnable;
        private string _proxyIp;
        private string _proxyPort;
        private string _proxyOverride;
        private string _toggleText;
        private string _t;

        public string TTT
        {
            get { return _t; }
            set
            {
                _t = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TTT)));
            }
        }
        public string ToggleText
        {
            get { return _toggleText; }
            set
            {
                _toggleText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToggleText)));
            }
        }
        public int ProxyEnable
        {
            get { return _proxyEnable; }
            set
            {
                _proxyEnable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProxyEnable)));
            }
        }
        public string ProxyIP
        {
            get { return _proxyIp; }
            set
            {
                _proxyIp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProxyIP)));
            }
        }
        public string ProxyPort
        {
            get { return _proxyPort; }
            set
            {
                _proxyPort = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProxyPort)));
            }
        }
        public string ProxyOverride
        {
            get { return _proxyOverride; }
            set
            {
                _proxyOverride = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProxyOverride)));
            }
        }
        public ICommand? ToggleProxy { get; set; }
        public ICommand? SaveConfig { get; set; }
        public ICommand? OpenSettingsDialog { get; set; }
        public ICommand? CloseDialogCommand { get; set; }



    }
}
