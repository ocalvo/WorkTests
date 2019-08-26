using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace SuperJupiter.Views
{
    public sealed partial class ScrollViewerView : Page
    {
        public ScrollViewerView()
        {
            this.InitializeComponent();
        }

        private void Options_Changed(object sender, RoutedEventArgs e)
        {
            if (tsIsTabStop != null)
            {
                sv.IsTabStop = tsIsTabStop.IsOn;
            }

            if (tsIsEngEnabled != null)
            {
                sv.IsFocusEngagementEnabled = tsIsEngEnabled.IsOn;
            }
        }

        private void On_CanvasButtonClicked(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            button.IsTabStop = !button.IsTabStop;
            if (!button.IsTabStop)
            {
                button.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                button.Foreground = new SolidColorBrush(Colors.White);
            }
        }
    }
}