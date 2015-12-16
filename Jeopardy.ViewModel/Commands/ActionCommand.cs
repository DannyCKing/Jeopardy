using System;
using System.Windows.Input;

namespace Jeopardy.ViewModel.Commands
{
    public class ActionCommand : ICommand
    {
        private Action<object> OnExecute;

        private Func<object, bool> CanExecuteAction;

        public ActionCommand(Action<object> action, Func<object, bool> canExec = null)
        {
            OnExecute = action;
            CanExecuteAction = canExec;
        }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteAction != null)
            {
                return CanExecuteAction(parameter);
            }
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            OnExecute(parameter);
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }
    }
}
