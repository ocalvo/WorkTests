using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;

namespace SuperJupiter
{

    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();

            this.contentFrame.Navigate(typeof(Views.SuperJupiterHomeView));
            if (this.RequestedTheme == ElementTheme.Default)
            {
                this.RequestedTheme = ElementTheme.Dark;
            }
            else
            {
                toggleTheme.IsOn = this.RequestedTheme == ElementTheme.Light ? true : false;
            }
        }


        private void toggleAppTheme(object sender, RoutedEventArgs e)
        {
            this.RequestedTheme = toggleTheme.IsOn ? ElementTheme.Light : ElementTheme.Dark;
        }

        private void toggleMouseMode(object sender, RoutedEventArgs e)
        {
            this.RequiresPointer = mouseModeToggle.IsOn ? Windows.UI.Xaml.Controls.RequiresPointer.WhenFocused : Windows.UI.Xaml.Controls.RequiresPointer.Never;
        }

        private void contentFrame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            if (e.Parameter != null) Title.Text = e.Parameter as string;
            else Title.Text = "Home";

			if (this.contentFrame.CanGoBack)
			{
                backButton.IsEnabled = true;
			}
			else
			{
                backButton.IsEnabled = false;
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.contentFrame.CanGoBack)
            {
                this.contentFrame.GoBack();
            }
        }
    }
}
