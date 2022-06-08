using System;

using System.Windows.Input;

namespace WinProxyTool_WPF.Utils
{
    class Command : ICommand
    {
        public Action<object> ExecuteAction { get; set; }
        public Func<object, bool> CanExecuteAction { get; set; }
        public event EventHandler? CanExecuteChanged;
        //private readonly Action<object?> _execute;
        //private readonly Func<object?, bool> _canExecute;
        //public Command()
        //{ 
        //}

        //public Command(Action<object?>? execute) : this(execute, null)
        //{
        //}
        //public Command(Action<object?> execute, Func<object?, bool>? canExecute)
        //{
        //    if (execute is null) throw new ArgumentNullException(nameof(execute));

        //    _execute = execute;
        //    _canExecute = canExecute ?? (x => true);
        //}

        // {
        //    add
        //    {
        //        CommandManager.RequerySuggested += value;
        //    }
        //    remove
        //    {
        //        CommandManager.RequerySuggested -= value;
        //    }
        //}
        //public void Refresh() => CommandManager.InvalidateRequerySuggested();
        public bool CanExecute(object parameter) => true;
        //CanExecuteAction.Invoke(parameter);

        public void Execute(object? parameter) => ExecuteAction.Invoke(parameter);
    }
}


