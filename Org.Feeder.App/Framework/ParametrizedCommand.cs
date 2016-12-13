using System;

namespace Org.Feeder.App.Framework
{
    /// <summary>
    /// Implementation of <see cref="System.Windows.Input.ICommand" /> to route executing commands (with an argument) into a ViewModel or a CommandManager.
    /// </summary>
    /// <typeparam name="T">Type of the argument to be provided during the call.</typeparam>
    public class ParametrizedCommand<T> : System.Windows.Input.ICommand
    {
        private readonly Action<T> _executeAction;
        private readonly Func<T, bool> _canExecuteAction;

        public ParametrizedCommand(Action<T> executeAction) : this(executeAction, arg => true) { }

        public ParametrizedCommand(Action<T> executeAction, Func<T, bool> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (parameter is T)
                return _canExecuteAction((T)parameter);

            return false;
        }

        public void Execute(object parameter)
        {
            if (parameter is T)
                _executeAction((T)parameter);
        }

        protected virtual void EvaluateCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
