using GalaSoft.MvvmLight.Command;
using System;
using System.Diagnostics;
using System.Threading;
using WinProxyTool_WPF.Model;
using WinProxyTool_WPF.Utils;
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
                    sendCommand = new RelayCommand(() => _SaveProxyServer());
                }
                return sendCommand;
            }
            set
            {
                sendCommand = value;
            }
        }
        #endregion

        //private void ExcuteSendCommand()
        //{   //对话框的确认按钮
        //    Messenger.Default.Send<String>(mainModel.InputProxyIP + ":" + mainModel.InputProxyPort, "ViewAlert");
        //    Debug.WriteLine("MouseLeftButtonDown");
        //}

        public MainModel mainModel { get; set; } = new();

        WinRegTool winRegTool = new();
        public MainViewModel()
        {
            mainModel.ToggleProxy = new Command { ExecuteAction = obj => { _ToggleProxy(); } };
            mainModel.UpdataProxyStatu = new Command { ExecuteAction = _ => { _AutoUpdataProxyStatu(); } };
            mainModel.SaveProxyServer = new Command { ExecuteAction = _ => { _SaveProxyServer(); } };

            mainModel.ToggleOverride = new Command { ExecuteAction = _ => { _ToggleOverride(); } };
            mainModel.UpdataOverrideStatu = new Command { ExecuteAction = _ => { _AutoUpdataOverrideStatu(); } };
            mainModel.SaveOverride = new Command { ExecuteAction = _ => { _SaveOverride(); } };

            mainModel.Sync = new Command { ExecuteAction = _ => { _SyncDialog(); } };

            _AutoUpdataProxyStatu();
            _AutoUpdataOverrideStatu();
            _SyncDialog();
            UIUpdataThread();
        }
        //自动更新线程
        private void UIUpdataThread()
        {
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    _AutoUpdataProxyStatu();
                    _AutoUpdataOverrideStatu();
                }
            }).Start();
        }
        private void _SyncDialog()
        {
            mainModel.InputProxyIP = mainModel.ProxyIP;
            mainModel.InputProxyPort = mainModel.ProxyPort;
            mainModel.InputProxyOverride = mainModel.ProxyOverride.Replace("<local>;", "").Replace(";<local>", "").Replace("<local>", "");
        }

        private void _ToggleProxy()
        {
            _AutoUpdataProxyStatu();
            switch (mainModel.ProxyEnable)
            {
                case 0:
                    winRegTool.Set_ProxyEnable(1); break;
                case 1:
                    winRegTool.Set_ProxyEnable(0); break;
                default: break;
            }
            _AutoUpdataProxyStatu();
        }

        public void _AutoUpdataProxyStatu()
        {
            mainModel.ProxyEnable = winRegTool.Get_ProxyEnable();
            string ProxyServer = winRegTool.Get_ProxyServer() ?? "";
            mainModel.ProxyOverride = winRegTool.Get_ProxyOverride() ?? "";
            try
            {
                int index = ProxyServer.IndexOf("://");
                if (index == ProxyServer.IndexOf(":")) { ProxyServer = (string)ProxyServer[(index + 3)..]; }

                if (!ProxyServer.Contains("://", StringComparison.CurrentCulture))
                {
                    mainModel.ProxyIP = (string)ProxyServer.Split(':')[0];
                    mainModel.ProxyPort = int.Parse((string)ProxyServer.Split(':')[1]);
                }
                mainModel.IsSkipLocal = mainModel.ProxyOverride.Contains("<local>");
                if (mainModel.ProxyOverride.EndsWith(";"))
                {
                    mainModel.ProxyOverride = mainModel.ProxyOverride.Substring(0, mainModel.ProxyOverride.Length - 1);
                }
            }
            catch (Exception) { }
        }
        private void _SaveProxyServer()
        {
            string checkIP = mainModel.InputProxyIP == null || mainModel.InputProxyIP == "" ? "空值" : "[" + mainModel.InputProxyIP + "]";
            string checkPort = mainModel.InputProxyPort == null || mainModel.InputProxyPort == 0 ? "空值" : "[" + mainModel.InputProxyPort + "]";
            Debug.WriteLine("checkIP >>>" + checkIP);
            Debug.WriteLine("checkPort >>>" + checkPort);

            try
            {   //判空
                if (mainModel.InputProxyIP != null
                    && mainModel.InputProxyIP != ""
                    && mainModel.InputProxyIP.IndexOf(" ") == -1)
                {   //port
                    if (mainModel.InputProxyPort != null
                    && mainModel.InputProxyPort > 0
                    && mainModel.InputProxyPort < 65536)
                    {
                        Debug.WriteLine("执行保存逻辑...");
                        winRegTool.Set_ProxyServer(mainModel.InputProxyIP + ":" + mainModel.InputProxyPort);
                    }
                    else Debug.WriteLine("端口错误");
                }
                else Debug.WriteLine("IP错误");
            }
            catch (Exception e) { Debug.WriteLine("捕获异常>>>" + e); }
        }

        private void _ToggleOverride() { _SaveOverride(); }
        public void _AutoUpdataOverrideStatu()
        {
            mainModel.ProxyOverride = winRegTool.Get_ProxyOverride() != null ? winRegTool.Get_ProxyOverride() : "";
            mainModel.IsSkipLocal = mainModel.ProxyOverride.Contains("<local>");
        }
        public void _SaveOverride()
        {
            string? input = mainModel.InputProxyOverride;
            if (mainModel.SkipStatu)
            {
                if (!input.Contains("<local>")) { input = input + ";<local>"; }
            }
            else
            {
                if (input.Contains("<local>"))
                {
                    input = input.Replace("<local>;", "").Replace(";<local>", "").Replace("<local>", "");
                }
            }
            if (input.StartsWith(";"))
            { input = input.Substring(1, input.Length - 1); }
            if (input.EndsWith(";"))
            { input = input.Substring(0, input.Length - 2); }
            winRegTool.Set_ProxyOverride(input);
        }
    }
}
