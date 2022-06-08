using MaterialDesignThemes.Wpf;
using System;
using System.Diagnostics;
using WinProxyTool_WPF.Model;
using WinProxyTool_WPF.Utils;
using WinProxyTool_WPF.View;

namespace WinProxyTool_WPF.ViewModel
{
    internal class MainViewModel : DialogHost
    {
        public MainModel mainModel { get; set; } = new();

        WinRegTool winRegTool = new();
        public MainViewModel()
        {
            mainModel.ToggleProxy = new Command { ExecuteAction = obj => { _ToggleProxy(obj); } };
            mainModel.SaveConfig = new Command { ExecuteAction = _ => { _SaveConfig(_); } };
            mainModel.OpenSettingsDialog = new Command { ExecuteAction = _ => { _OpenSettingsDialog(_); } };
            mainModel.CloseDialogCommand = new Command { ExecuteAction = _ => { _CloseDialogCommand(_); } };

            UpdateStat();
            UIUpdataThread();
        }

        //public ICommand RunDialogCommand => new Command(ExecuteRunDialog);

        private void UIUpdataThread()
        {
            //  new Thread(() => { while (true) { Thread.Sleep(1000); UpdateStat(); } }).Start();
        }

        private void _ToggleProxy(object? _)
        {
            UpdateStat();
            switch (mainModel.ProxyEnable)
            {
                case 0:
                    winRegTool.Set_ProxyEnable(1); break;
                case 1:
                    winRegTool.Set_ProxyEnable(0); break;
                default: break;
            }
            UpdateStat();
        }
        private void _SaveConfig(object? o)
        {
            mainModel.ProxyIP = mainModel.ProxyIP.Trim();
            mainModel.ProxyPort = mainModel.ProxyPort.Trim();
            if (mainModel.ProxyIP != "" && mainModel.ProxyPort.IndexOf(" ") == -1)
            {
                if (mainModel.ProxyPort != "" && mainModel.ProxyPort.IndexOf(" ") == -1 && int.TryParse(mainModel.ProxyPort, out _))
                {
                    Debug.WriteLine("执行保存逻辑");
                    winRegTool.Set_ProxyServer(mainModel.ProxyIP + ":" + mainModel.ProxyPort);
                }
                else
                {
                    Debug.WriteLine("端口错误");
                }
            }
            else
            {
                Debug.WriteLine("IP错误");
            }
        }

        private async void _OpenSettingsDialog(object? _)
        {
            UpdateStat();
            //let's set up a little MVVM,` cos that's what the cool kids are doing:
            // var view = new MainWindow
            {
                //     DataContext = new MainViewModel()
            };

            //show the dialog
            //var result = await Show(view, ClosingEventHandler);
            // var result = await Show(this, ClosingEventHandler);


            //check the result...
            //  Debug.WriteLine("对话框已关闭, 参数为: " + (result ?? "空值"));
            //  Debug.WriteLine("view.Name:->" + "" + "<-");
        }
        private void _CloseDialogCommand(object _)
        {

        }


        public void UpdateStat()
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


        public bool CanDoSomething(object _) { return true;  /**判断能否做这个事情，大部分时候返回true就行了**/ }


        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
            => Debug.WriteLine("对话框关闭事件回调");

    }
}
