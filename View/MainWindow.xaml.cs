using MaterialDesignThemes.Wpf;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Debug.WriteLine("Button_Click 按钮点击事件");
           // MessageBoxResult result = MessageBox.Show("111");
          //  MessageQueueExtension messageQueueExtension = new MessageQueueExtension();
           // Debug.WriteLine(result);

        }
    }
}
