using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class CommandBarView : Page
    {
        public CommandBarView()
        {
            this.InitializeComponent();
        }

        private void toggleCommandBar(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            commandBar.IsOpen = !commandBar.IsOpen;
        }

        private void gamepadCommandBar(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.NavigationMenu)
            {
                commandBar.IsOpen = !commandBar.IsOpen;
            }
        }
    }
}
