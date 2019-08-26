using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class WebView : Page
    {
        public WebView()
        {
            this.InitializeComponent();
            this.NavigateWebview();
        }

        private void NavigateWebview()
        {
            try
            {
                Uri targetUri = new Uri("http://www.nbthieves.com/");
                webView1.Navigate(targetUri);
            }
            catch (FormatException myE)
            {
                // Bad address
                webView1.NavigateToString(String.Format("<h1>Address is invalid, try again.  Details --> {0}.</h1>", myE.Message));
            }
        }

        private void goToWebpage(object sender, RoutedEventArgs e)
        {
            try
            {
                Uri targetUri = new Uri(addressBox.Text);
                webView1.Navigate(targetUri);
            }
            catch (Exception myE)
            {
                // Bad address
                webView1.NavigateToString(String.Format("<h1>Address is invalid, try again.  Details --> {0}.</h1>", myE.Message));
            }
        }
    }
}

