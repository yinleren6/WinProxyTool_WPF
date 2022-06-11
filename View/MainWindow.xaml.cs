using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using WinProxyTool_WPF.ViewModel;
namespace WinProxyTool_WPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainViewModel();

            Messenger.Default.Register<string>(this, "ViewAlert", ShowReceiveInfo);

            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);

            InitializeComponent();
        }

        private void ShowReceiveInfo(string msg)
        {
            Debug.WriteLine("对话框 确认按钮");
            Debug.WriteLine(msg);
        }

        private void Github(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://github.com/yinleren6/WinProxyTool_WPF");

        }
    }
}
