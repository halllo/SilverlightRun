using System;
using System.Windows.Input;

namespace SilverlightRun.ViewModel
{
    /// <summary>
    /// Basic command for parameterless actions.
    /// </summary>
    public class ColdCommand : ICommand
    {
        Action _com;
        bool _exec;

        public ColdCommand(Action command)
        {
            _exec = true;
            _com = command;
        }

        public void SetAsExecutable(bool exec)
        {
            _exec = exec;
            CanExecuteChanged(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return _exec;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _com();
        }
    }
}
