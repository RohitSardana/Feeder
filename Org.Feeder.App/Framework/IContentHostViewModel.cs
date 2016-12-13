using Org.Feeder.App.ViewModels;

namespace Org.Feeder.App.Framework
{
    public interface IContentHostViewModel : IViewModel
    {
        IViewModel Content { get; set; }
    }
}