using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;
using WinProxyTool_WPF.Model;
using WinProxyTool_WPF.Utils;
using WinProxyTool_WPF.View;
namespace WinProxyTool_WPF.ViewModel
{

    internal class MainViewModel : ViewModelBase
    {
        #region 命令
        private RelayCommand sendCommand;

        public RelayCommand _sendCommand
        {
            get
            {
                if (sendCommand == null)
                {
                    sendCommand = new RelayCommand(() => _SaveConfig());
                }
                return sendCommand;
            }
            set
            {
                sendCommand = value;
            }
        }
        #endregion

        private void ExcuteSendCommand()
        {   //对话框的确认按钮
            Messenger.Default.Send<String>(mainModel.InputProxyIP + ":" + mainModel.InputProxyPort, "ViewAlert");
            Debug.WriteLine("MouseLeftButtonDown");
        }

        public MainModel mainModel { get; set; } = new();

        WinRegTool winRegTool = new();
        public MainViewModel()
        {
            mainModel.ToggleProxy = new Command { ExecuteAction = obj => { _ToggleProxy(obj); } };
            mainModel.SaveConfig = new Command { ExecuteAction = _ => { _SaveConfig(); } };
            mainModel.UpdateStat = new Command { ExecuteAction = _ => { _updateStat(); } };
            mainModel.Sync = new Command { ExecuteAction = _ => { _sync(); } };
            mainModel.SaveConfig = _sendCommand;

            _updateStat();
            UIUpdataThread();
        }

        private void _sync()
        {
            mainModel.InputProxyIP = mainModel.ProxyIP;
            mainModel.InputProxyPort = mainModel.ProxyPort;
        }

        //自动更新线程
        private void UIUpdataThread()
        {
            new Thread(() => { while (true) { Thread.Sleep(1000); _updateStat(); } }).Start();
        }

        private void _ToggleProxy(object? _)
        {
            _updateStat();
            switch (mainModel.ProxyEnable)
            {
                case 0:
                    winRegTool.Set_ProxyEnable(1); break;
                case 1:
                    winRegTool.Set_ProxyEnable(0); break;
                default: break;
            }
            _updateStat();
        }

        private void _SaveConfig()
        {
            Debug.WriteLine("InputIP=" + (mainModel.InputProxyIP == null ? "空值" : ">" + mainModel.InputProxyIP + "<"));
            Debug.WriteLine("InputPort=" + (mainModel.InputProxyPort == null ? "空值" : ">" + mainModel.InputProxyPort + "<"));


            if (mainModel.InputProxyIP != "" && mainModel.InputProxyIP.IndexOf(" ") == -1)
            {
                if (mainModel.InputProxyPort != "" && mainModel.InputProxyPort.IndexOf(" ") == -1 && int.TryParse(mainModel.InputProxyPort, out _))
                {
                    Debug.WriteLine("执行保存逻辑");
                    winRegTool.Set_ProxyServer(mainModel.InputProxyIP + ":" + mainModel.InputProxyPort);
                }
                else { Debug.WriteLine("端口错误"); }
            }
            else { Debug.WriteLine("IP错误"); }
        }

        private async void _OpenSettingsDialog(object? _)
        {
            _updateStat();

            var view = new MainWindow
            {
                DataContext = new MainViewModel()
            };

            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);

            Debug.WriteLine("对话框已关闭, 参数为: " + (result ?? "空值"));

        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs) => Debug.WriteLine("对话框关闭事件回调");

        public void _updateStat()
        {
            mainModel.ProxyEnable = winRegTool.Get_ProxyEnable();
            mainModel.ToggleText = mainModel.ProxyEnable == 1 ? "开" : "关";

            string ProxyServer = winRegTool.Get_ProxyServer();
            try
            {
                int index = ProxyServer.IndexOf("://");
                if (index == ProxyServer.IndexOf(":")) { ProxyServer = (string)ProxyServer[(index + 3)..]; }

                if (!ProxyServer.Contains("://", StringComparison.CurrentCulture))
                {
                    mainModel.ProxyIP = (string)ProxyServer.Split(':')[0];
                    mainModel.ProxyPort = (string)ProxyServer.Split(':')[1];
                }
            }
            catch (Exception) { }
            mainModel.ProxyOverride = winRegTool.Get_ProxyOverride();

        }
        

    }
}
