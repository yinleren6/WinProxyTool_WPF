using System;
using System.Windows.Input;

namespace WinProxyTool_WPF.Utils
{
    class Command : ICommand
    {
        public Action<object>? ExecuteAction { get; set; }
        public Func<object, bool>? CanExecuteAction { get; set; }
        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter) => ExecuteAction.Invoke(parameter);
    }
}
