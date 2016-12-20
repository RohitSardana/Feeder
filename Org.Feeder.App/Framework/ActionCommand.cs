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

        /// <summary>
        /// Initializes the ActionCommand with action to be executed. Sets the canExecuteAction to true.
        /// </summary>
        /// <param name="executeAction"></param>
        public ActionCommand(Action executeAction) : this(executeAction, () => true) { }

        /// <summary>
        /// Initializes the ActionCommand with action to be executed and with a function to check whether this action can be executed or not.
        /// </summary>
        /// <param name="executeAction"></param>
        /// <param name="canExecuteAction"></param>
        public ActionCommand(Action executeAction, Func<bool> canExecuteAction)
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
            return _canExecuteAction();
        }

        /// <summary>
        /// Executes the action underlying the command.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _executeAction();
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