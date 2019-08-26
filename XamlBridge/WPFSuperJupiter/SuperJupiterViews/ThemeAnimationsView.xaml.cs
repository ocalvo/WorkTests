using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace SuperJupiter.Views
{
    public sealed partial class ThemeAnimationsView : Page
    {
        public ThemeAnimationsView()
        {
            this.InitializeComponent();
        }

        private void DragItemThemeAnimation_Click(object sender, RoutedEventArgs e) { StartStoryboard("dragitem"); }

        private void DragOverThemeAnimation_Click(object sender, RoutedEventArgs e) { StartStoryboard("dragover"); }

        private void DropTargetItemThemeAnimation_Click(object sender, RoutedEventArgs e) { StartStoryboard("droptargetitem"); }

        private void RepositionThemeAnimation_Click(object sender, RoutedEventArgs e) { StartStoryboard("reposition"); }

        private void FadeInThemeAnimation_Click(object sender, RoutedEventArgs e) { StartStoryboard("fadein"); }

        private void FadeOutThemeAnimation_Click(object sender, RoutedEventArgs e) { StartStoryboard("fadeout"); }

        private void PointerDownThemeAnimation_Click(object sender, RoutedEventArgs e) { StartStoryboard("pointerdown"); }

        private void PointerUpThemeAnimation_Click(object sender, RoutedEventArgs e) { StartStoryboard("pointerup"); }

        private void PopInThemeAnimation_Click(object sender, RoutedEventArgs e) { StartStoryboard("popin"); }

        private void PopOutThemeAnimation_Click(object sender, RoutedEventArgs e) { StartStoryboard("popout"); }

        private void SplitOpenThemeAnimation_Click(object sender, RoutedEventArgs e) { (sender as Button).IsEnabled = false; splitCloseBtn.IsEnabled = true; popup.IsOpen = true; StartStoryboard("splitopen"); }

        private void SplitCloseThemeAnimation_Click(object sender, RoutedEventArgs e) { (sender as Button).IsEnabled = false; StartStoryboard("splitclose"); }

        private void SwipeBackThemeAnimation_Click(object sender, RoutedEventArgs e) { StartStoryboard("swipeback"); }

        private void SwipeHintThemeAnimation_Click(object sender, RoutedEventArgs e) { StartStoryboard("swipehint"); }

        private void StartStoryboard(string storyboardName)
        {
            Storyboard sb = (Storyboard)hostCanvas.FindName(storyboardName);
            if (sb != null)
            {
                sb.Begin();
            }
        }

        private void popup1(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }
    }
}
