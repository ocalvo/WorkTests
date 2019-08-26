using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WebViewLeak
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private WebView webView;

        private void OnCreate(object sender, RoutedEventArgs e)
        {
            this.webView = new WebView();
            webView.Width = 250;
            webView.Height = 250;
            this.rootPanel.Children.Add(webView);

            //webView.UnsafeContentWarningDisplaying += WebView_UnsafeContentWarningDisplaying;

            webView.Navigate(new Uri("http://example.com/67d209a2-e272-4462-8eed-ea25cd83e4e6"));
        }

        private void OnClear(object sender, RoutedEventArgs e)
        {
            this.rootPanel.Children.Remove(this.webView);
            //webView.UnsafeContentWarningDisplaying -= WebView_UnsafeContentWarningDisplaying;
            this.webView = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void WebView_UnsafeContentWarningDisplaying(WebView sender, object args)
        {
            //throw new NotImplementedException();
        }
    }
}
