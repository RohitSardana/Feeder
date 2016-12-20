namespace Org.Feeder.App.ViewModels.SampleData
{
    /// <summary>
    /// Represents sample data for Error screen
    /// </summary>
    public class ErrorViewModelSampleData : ErrorViewModel
    {
        /// <summary>
        /// Initializes sample data for Error screen
        /// </summary>
        public ErrorViewModelSampleData() : base("Oops!", "It seems something nefarious has happened...", null, "Dismiss")
        {
        }
    }
}