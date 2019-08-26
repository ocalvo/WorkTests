using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SuperJupiter.Views
{
    public sealed partial class SuperJupiterHomeView : Page
    {
        public SuperJupiterHomeView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.Loaded += SuperJupiterHomeView_Loaded;
        }

        private void SuperJupiterHomeView_Loaded(object sender, RoutedEventArgs e)
        {
            if (lastFocusIndex >= 0 && lastFocusIndex < optionsPanel.Children.Count)
            {
                var element = optionsPanel.Children[lastFocusIndex] as Control;
                if (element != null)
                {
                    element.Focus(FocusState.Keyboard);
                }
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            var element = FocusManager.GetFocusedElement() as UIElement;
            if (element != null)
            {
                lastFocusIndex = optionsPanel.Children.IndexOf(element);
            }
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

        int lastFocusIndex = -1;
    }
}
