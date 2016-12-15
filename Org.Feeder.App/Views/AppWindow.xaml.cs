using Org.Feeder.App.Framework;
using System;
using System.Windows.Media.Imaging;

namespace Org.Feeder.App.Views
{
    public partial class AppWindow : IWindow
    {
        public AppWindow()
        {
            InitializeComponent();
            Uri iconUri = new Uri(@"../../Resources/Images/Feeder.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }
    }
}
