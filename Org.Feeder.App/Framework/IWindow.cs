namespace Org.Feeder.App.Framework
{
    public interface IWindow
    {
        object DataContext { get; set; }
        void Show();
    }
}