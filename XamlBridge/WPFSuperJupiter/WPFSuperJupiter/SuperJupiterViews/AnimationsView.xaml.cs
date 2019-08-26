using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class AnimationsView : Page
    {
        public AnimationsView()
        {
            this.InitializeComponent();
        }

        private void navigateToControl(object sender, RoutedEventArgs e)
        {
            Button senderButton = sender as Button;

            string pageName = senderButton.Name;
            pageName = "SuperJupiter.Views." + pageName + "View";

            Type pageType = Type.GetType(pageName);

            if (pageType != null)
            {
                this.Frame.Navigate(pageType, senderButton.Content);
            }
        }
    }
}
