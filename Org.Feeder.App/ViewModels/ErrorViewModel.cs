using System;
using Org.Feeder.App.Framework;

namespace Org.Feeder.App.ViewModels
{
    public class ErrorViewModel : ViewModelBase
    {
        public ErrorViewModel(string title, string message, Action tryAgainAction)
        {
            Title = title;
            Message = message;

            DismissCommand = new ActionCommand(
                tryAgainAction);
        }

        public string Title { get; private set; }
        public string Message { get; private set; }

        public ActionCommand DismissCommand { get; private set; }
    }
}