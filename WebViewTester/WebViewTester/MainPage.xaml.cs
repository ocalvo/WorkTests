using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WebViewTester
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private WebView webView;

        public MainPage()
        {
            this.InitializeComponent();
            this.bt1.Focus(FocusState.Keyboard);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

        }

        private void OnCreate(object sender, RoutedEventArgs e)
        {
            if (this.webView == null)
            {
                this.webView = new WebView(WebViewExecutionMode.SameThread)
                {
                    //Source = new Uri("ms-appx-web:///Assets/superhotvr.html"),
                    Source = new Uri("ms-appx-web:///Assets/input.html"),
                    Width = 600.0,
                    Height = 100.0,
                };
                this.root.Children.Add(this.webView);
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            if (this.webView != null)
            {
                this.root.Children.Remove(this.webView);
                this.webView = null;
                GC.Collect();
                GC.Collect();
            }
        }
    }
}
