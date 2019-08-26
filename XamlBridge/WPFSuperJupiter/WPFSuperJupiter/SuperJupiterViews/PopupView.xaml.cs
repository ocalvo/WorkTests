using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class PopupBaseView : Page
    {
        public PopupBaseView()
        {
            this.InitializeComponent();
        }

        private void OnTogglePop01(object sender, RoutedEventArgs e)
        {
            this.popup1.IsOpen = !this.popup1.IsOpen;
        }
    }
}

