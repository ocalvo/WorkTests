using MediaCaptureTester.MainPage.xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Windows.UI.Core
{
    enum NavigationReason
    {
    }

    class NavigationFocusEventArgs
    {
    }

    class FocusResult
    {

    }

    interface ISiteFocus
    {
        NavigationFocusEventArgs CreateNavigationFocusEventArgs(NavigationReason reason, Windows.Foundation.Rect origin);
        async Task<FocusResult> NavigateFocusAsync(NavigationFocusEventArgs args);
        event FocusDeparting(ISiteFocus sender, NavigationFocusEventArgs args);
    }
}
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MediaCaptureTester
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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var mp = new MediaCapture();
            await mp.InitializeAsync();
        }
    }
}

namespace MediaCaptureTester.MainPage.xaml
{
    enum NavigationReason
    {
    }
}