using System;

using System.Windows.Input;

namespace WinProxyTool_WPF.Utils
{
    class Command : ICommand
    {
        public Action<object> ExecuteAction { get; set; }
        public Func<object, bool> CanExecuteAction { get; set; }
        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object parameter) => true;
        //CanExecuteAction.Invoke(parameter);

        public void Execute(object? parameter) => ExecuteAction.Invoke(parameter);


        #region------------------------------------------

        //private readonly Action<object> _executeAction;

        //public Command(Action<object> executeAction)
        //{
        //    _executeAction = executeAction;
        //}

        //public void Execute(object parameter) => _executeAction(parameter);

        //public bool CanExecute(object parameter) => true;

        //public event EventHandler CanExecuteChanged;

        #endregion-----------------------------------------------


    }
}




