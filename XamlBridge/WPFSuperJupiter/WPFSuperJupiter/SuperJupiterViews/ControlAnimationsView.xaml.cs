using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class ControlAnimationsView : Page
    {
        public ControlAnimationsView()
        {
            this.InitializeComponent();
        }

        //private void DoSwivle(object sender, RoutedEventArgs e) { var ignore = swivleContentDialog.ShowAsync(); }

        //private void DoReaderboard(object sender, RoutedEventArgs e) { var ignore = readerboardContentDialog.ShowAsync(); }

        private void DoEscalator(object sender, RoutedEventArgs e) { Flyout.GetAttachedFlyout((FrameworkElement)sender).ShowAt((FrameworkElement)sender); }
    }
}
