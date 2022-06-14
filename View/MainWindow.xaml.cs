using System;
using System.Diagnostics;
using System.Windows;
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
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void Github(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/yinleren6/WinProxyTool_WPF");
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);//关闭所有线程
            if (MessageBox.Show("确定是否关闭当前应用程序？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {  //base.OnClosing(e);
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
                //base.Hide(); 
            }
        }
    }
}
