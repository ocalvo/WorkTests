using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class SplitView : Page
    {
        private bool _splitViewLoaded = false;

        public SplitView()
        {
            this.InitializeComponent();
        }

        private void togglePaneButton_Click(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private void DisplayModeRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            if(!_splitViewLoaded)
                return;

            RadioButton rb = sender as RadioButton;
            SplitViewDisplayMode newDisplayMode = (SplitViewDisplayMode)Enum.Parse(typeof(SplitViewDisplayMode), rb.Content as string);
            splitView.DisplayMode = newDisplayMode;
        }

        private void SplitView_Loaded(object sender, RoutedEventArgs e)
        {
            _splitViewLoaded = true;
        }
    }
}