using System;

namespace Org.Feeder.App.Framework
{
    /// <summary>
    /// Implementation of <see cref="System.Windows.Input.ICommand"/> to route executing commands (without arguments) into a ViewModel or a CommandManager.
    /// </summary>
    public class ActionCommand : System.Windows.Input.ICommand
    {
        private readonly Action _executeAction;
        private readonly Func<bool> _canExecuteAction;

        public ActionCommand(Action executeAction) : this(executeAction, () => true) { }
        public ActionCommand(Action executeAction, Func<bool> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecuteAction();
        }

        public void Execute(object parameter)
        {
            _executeAction();
        }

        protected virtual void EvaluateCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}