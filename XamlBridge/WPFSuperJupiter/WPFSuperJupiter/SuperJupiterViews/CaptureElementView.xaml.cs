using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using Windows.Media.Capture;
using Windows.UI.Xaml.Navigation;

namespace SuperJupiter.Views
{
    public sealed partial class CaptureElementView : Page
    {
        private MediaCapture mediaCaptureMgr;


        public CaptureElementView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            StopPreview();
        }

        internal async void StopPreview()
        {
            try
            {
                if (mediaCaptureMgr != null)
                {
                    await mediaCaptureMgr.StopPreviewAsync();
                    mediaCaptureMgr = null;
                }
            }
            catch (Exception exception)
            {
                // Exception handler as needed
                exceptionText.Text = exception.Message;
            }
        }

        internal async void ShowPreview()
        {
            try
            {
                if (mediaCaptureMgr == null)
                {

                    mediaCaptureMgr = new MediaCapture();
                    await mediaCaptureMgr.InitializeAsync();

                    myCaptureElement.Source = mediaCaptureMgr;
                    await mediaCaptureMgr.StartPreviewAsync();
                }
            }
            catch (Exception exception)
            {
                // Exception handler as needed
                exceptionText.Text = exception.Message;
            }
        }
        
        private void showVideo(object sender, RoutedEventArgs e)
        {
            this.ShowPreview();
        }

        private void stopVideo(object sender, RoutedEventArgs e)
        {
            StopPreview();
        }
    }
}
