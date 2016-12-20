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

        /// <summary>
        /// Initializes the ParameterizedCommand with action to be executed and with a function which sets canExecute to true.
        /// </summary>
        public ParametrizedCommand(Action<T> executeAction) : this(executeAction, arg => true) { }

        /// <summary>
        /// Initializes the ParameterizedCommand with action to be executed and with a function which determines whether action can be executed or not.
        /// </summary>
        public ParametrizedCommand(Action<T> executeAction, Func<T, bool> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        /// <summary>
        /// Represents an event to notify its observers that CanExecute property for the command is changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Finds if the action can be executed or not.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (parameter is T)
                return _canExecuteAction((T)parameter);

            return false;
        }

        /// <summary>
        /// Executes the action underlying the command.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (parameter is T)
                _executeAction((T)parameter);
        }

        /// <summary>
        /// Fires the CanExecuteChanged event if not null.
        /// </summary>
        protected virtual void EvaluateCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
