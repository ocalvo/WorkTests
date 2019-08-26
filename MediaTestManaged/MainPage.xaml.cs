using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Tests.Common;

public static class MediaData
{
    public const string PosterUrlStr = "http://1.bp.blogspot.com/-jlAmGweWyRw/T9nfCve5VAI/AAAAAAAAF2s/bYrmAuarUxU/s1600/Star-wars-wallpaper-26.jpg";
    //const string videoLadyBugUrlStr = "http://jolt-media/video/ladybug.wmv";
    //const string video4UrlStr = "http://jolt-media/video/office_intro.mp4";
    //const string video5UrlStr = "http://jolt-media/video/test5.wmv";
    const string video42UrlStr = "ms-appx:///Assets/42.mkv";
    const string videoMatroskaUrlStr = "ms-appx:///Assets/matroska_test_w1_1/test5.mkv";
    const string video4KUrlStr = "ms-appx:///Assets/samsung_seven_wonders_of_the_world_china_uhd-DWEU.mkv";
    const string audio1UrlStr = "ms-appx:///Assets/TakeOnMe.mp3";
    const string videoBlueFrameUrlStr = "ms-appx:///Assets/blueframe_video.mp4";
    const string videoLiveCaptioning = "https://lw.bamgrid.com/2.0/hls/vod/nocookie/profile/default/bam/qs03/hls/sony/unenc/master.m3u8";

    //"http://jolt-media/video/bear.wmv";
    //"http://jolt-media/ocalvo/matroska_test_w1_1/test1.mkv";
    //"http://jolt-media/video/test5.wmv";
    // 4 Closed captions
    // 3 The Office
    public static readonly string[] VideoUrls =
    {
        video42UrlStr,
        audio1UrlStr,
        videoBlueFrameUrlStr,
        videoMatroskaUrlStr,
        video4KUrlStr,
        videoLiveCaptioning,
    };
    const int videoIndex = 5;

    public class SourceDesc
    {
        public SourceDesc(string name, object source)
        {
            this.Name = name;
            this.Source = source;
        }

        public bool IsAdaptive = false;

        public string Name { get; private set; }
        public object Source { get; private set; }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public static readonly SourceDesc[] Sources = new SourceDesc[]
    {
        new SourceDesc("Matroska", videoMatroskaUrlStr),
        //new SourceDesc("Ladybug", video2UrlStr),
        //new SourceDesc("The Office", video4UrlStr),
        new SourceDesc("42 Subtitle",  video42UrlStr),
        new SourceDesc("4K",  video4KUrlStr),
        new SourceDesc("Audio",  audio1UrlStr),
        new SourceDesc("Playlist",  VideoUrls),
        new SourceDesc("BlueFrame", videoBlueFrameUrlStr),
        new SourceDesc("LiveCaptioning", videoLiveCaptioning) { IsAdaptive = true, },
    };
}

namespace MediaTestManaged
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.m = defaultObj;
            foreach (var s in MediaData.Sources)
            {
                this.sourceBox.Items.Add(s);
            }
            this.sourceBox.SelectedIndex = 0;
            this.inkCanvas.InkPresenter.InputDeviceTypes = Windows.UI.Core.CoreInputDeviceTypes.Mouse | Windows.UI.Core.CoreInputDeviceTypes.Touch | Windows.UI.Core.CoreInputDeviceTypes.Pen;
        }

        private Stretch GetStretchMode(int index)
        {
            switch (index)
            {
                case 0:
                    return Stretch.Fill;
                case 1:
                    return Stretch.None;
                case 2:
                    return Stretch.Uniform;
                case 3:
                    return Stretch.UniformToFill;
            }

            return Stretch.Fill;
        }

        const double scale = 1.75;

        private UIElement m = null;

        private async Task InitializeMPE()
        {
            var mpe = new Windows.UI.Xaml.Controls.MediaPlayerElement();
            if (sizeChk.IsChecked.Value)
            {
                mpe.Width = 200.0 * scale;
                mpe.Height = 170.0 * scale;
            }

            m = mpe;

            if (this.enableMTC.IsChecked.Value)
            {
                mpe.AreTransportControlsEnabled = true;
                mpe.AutoPlay = false;
                if (IsPlayList)
                {
                    mpe.TransportControls.IsNextTrackButtonVisible = true;
                    mpe.TransportControls.IsPreviousTrackButtonVisible = true;
                    /*
                    mpe.TransportControls.IsPlaybackRateButtonVisible = true;
                    mpe.TransportControls.IsFastForwardButtonVisible = true;
                    mpe.TransportControls.IsFastRewindButtonVisible = true;
                    mpe.TransportControls.IsFullWindowButtonVisible = true;
                    mpe.TransportControls.IsSkipBackwardButtonVisible = true;
                    mpe.TransportControls.IsSkipForwardButtonVisible = true;
                    mpe.TransportControls.IsStopButtonVisible = true;
                    mpe.TransportControls.IsVolumeButtonVisible = true;
                    mpe.TransportControls.IsZoomButtonVisible = true;
                    */
                }
            }
            else
            {
                mpe.AutoPlay = true;
                mpe.TransportControls = null;
                mpe.AreTransportControlsEnabled = false;
            }

            mpe.AutoPlay = this.autoPlay.IsChecked.Value;

            mpe.Stretch = GetStretchMode(this.stretchMode.SelectedIndex);

            if (this.enableFullWindow.IsChecked.Value)
            {
                mpe.IsFullWindow = true;
            }

            if (this.poster.IsChecked.Value)
            {
                var bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
                var urlStr = MediaData.PosterUrlStr;
                bitmapImage.UriSource = new Uri(urlStr);
                mpe.PosterSource = bitmapImage;
            }

            if (this.extPlayer.IsChecked.Value)
            {
                var mp = new MediaPlayer();
                mpe.SetMediaPlayer(mp);
            }
            else
            {
                mpe.Source = await CreateSource();
            }

            MediaPanel.Children.Add(mpe);
            await Dispatcher.RunIdleAsync((v) =>
            {
                presenter = VisualTreeUtils.FindElementOfTypeInSubtree<MediaPlayerPresenter>(mpe);
            });

            //VideoElementContainer.Content = mpe;

            Log.Text = "made it";

            mpe.Tapped += OnTapped;
            mpe.PointerPressed += this.OnPointerPressed;
            mpe.DoubleTapped += this.OnDoubleTapped;
            //mpe.TransportControls.DoubleTapped += this.OnDoubleTapped;
        }

        bool IsPlayList
        {
            get
            {
                var sourceData = (MediaData.SourceDesc)this.sourceBox.SelectedItem;
                return !(sourceData.Source is string);
            }
        }

        private async Task<IMediaPlaybackSource> CreateSource()
        {
            IMediaPlaybackSource source = null;
            var sourceData = (MediaData.SourceDesc)this.sourceBox.SelectedItem;
            if (!IsPlayList)
            {
                var uri = new Uri(sourceData.Source.ToString());
                if (sourceData.IsAdaptive)
                {
                    var result = await Windows.Media.Streaming.Adaptive.AdaptiveMediaSource.CreateFromUriAsync(uri);
                    source = Windows.Media.Core.MediaSource.CreateFromAdaptiveMediaSource(result.MediaSource);
                }
                else
                {
                    source = new Windows.Media.Playback.MediaPlaybackItem(Windows.Media.Core.MediaSource.CreateFromUri(new Uri((sourceData.Source.ToString()))));
                }
            }
            else
            {
                var urls = (string[])sourceData.Source;
                var sources = urls.Select(url => Windows.Media.Core.MediaSource.CreateFromUri(new Uri(url)));
                var items = sources.Select(s => new MediaPlaybackItem(s));
                var list = new MediaPlaybackList();
                list.Items.Clear();
                foreach (var i in items)
                {
                    list.Items.Add(i);
                }
                source = list;
            }
            return source;
        }

        private void OnDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            this.OnFullWindow();
            //this.OnClear(sender, null);
        }

        private void OnFullWindow()
        {
            var mpe = m as MediaPlayerElement;
            if (mpe != null)
            {
                mpe.IsFullWindow = !mpe.IsFullWindow;
            }
            var me = m as MediaElement;
            if (me != null)
            {
                me.IsFullWindow = !me.IsFullWindow;
            }
        }

        private void OnFullWindow(object sender, Windows.UI.Xaml.RoutedEventArgs evargs)
        {
            this.OnFullWindow();
        }

        private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
        }

        static MediaPlayerPresenter presenter;

        private static FrameworkElement GetFullWindowMediaRoot(MediaPlayerPresenter presenter)
        {
            var p = presenter as FrameworkElement;
            while (p.Parent != null)
            {
                p = p.Parent as FrameworkElement;
            }
            if (p!=null && p.Name=="LayoutRoot")
            {
                return null; //retry later, Xaml tree is in transition
            }

            return p as Panel;
        }

        private static IEnumerable<DependencyObject> GetAllChildren(DependencyObject root)
        {
            yield return root;

            var count = VisualTreeHelper.GetChildrenCount(root);
            for(int i = 0;i<count;i++)
            {
                var child = VisualTreeHelper.GetChild(root, i);
                var allChildren = GetAllChildren(child);
                foreach(var c in allChildren)
                {
                    yield return c;
                }
            }
        }

        private void RefreshMediaAP()
        {
            var r = Window.Current.Content;
            var rootUI = GetAllChildren(r)
                .OfType<UIElement>()
                .First(ui => ui!=null && FrameworkElementAutomationPeer.CreatePeerForElement(ui) != null);
            var apRoot = FrameworkElementAutomationPeer.CreatePeerForElement(rootUI as UIElement);
            if (apRoot!=null)
            {
                if (apRoot.GetChildren().Count == 0)
                {
                    throw new InvalidProgramException();
                }
                var apParent = apRoot.Navigate(AutomationNavigationDirection.Parent) as AutomationPeer;
                if (apParent != null)
                {
                    if (apParent.GetChildren().Count == 0)
                    {
                        throw new InvalidProgramException();
                    }
                }
            }

            var root = GetFullWindowMediaRoot(presenter);
            if (root == null && m != null)
            {
                root = m as FrameworkElement;
            }
            if (root != null)
            {
                var ap = FrameworkElementAutomationPeer.CreatePeerForElement(root);
                while (ap == null && root != null)
                {
                    root = root.Parent as FrameworkElement;
                    if (root != null)
                    {
                        ap = FrameworkElementAutomationPeer.CreatePeerForElement(root);
                    }
                }
                if (ap != null)
                {
                    if (ap.GetChildren().Count == 0)
                    {
                        throw new InvalidProgramException();
                    }
                }
            }
        }

        private async void OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var mpe = m as MediaPlayerElement;
            if (mpe != null)
            {
                await Dispatcher.RunIdleAsync((v) =>
                {
                    RefreshMediaAP();
                });
            }
        }

        private object CreateSourceME()
        {
            var sourceData = (MediaData.SourceDesc)this.sourceBox.SelectedItem;
            if (sourceData.Source is string)
            {
                return new Uri(sourceData.Source.ToString());
            }
            else
            {
                return this.CreateSource() as IMediaPlaybackSource;
            }
        }

        void InitializeME()
        {
            var me = new MediaElement();
            m = me;

            var s = CreateSourceME();
            if ( s is Uri)
            {
                me.Source = (Uri)s;
            }
            else
            {
                me.SetPlaybackSource(s as IMediaPlaybackSource);
            }

            if (this.enableMTC.IsChecked.Value)
            {
                me.AreTransportControlsEnabled = true;
                me.TransportControls.IsNextTrackButtonVisible = true;
                me.TransportControls.IsPreviousTrackButtonVisible = true;
                //me.TransportControls.IsStopEnabled = true;
                me.AutoPlay = false;
            }
            if (sizeChk.IsChecked.Value)
            {
                me.Width = 200.0 * scale;
                me.Height = 170.0 * scale;
            }

            if (this.poster.IsChecked.Value)
            {
                var bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
                var urlStr = MediaData.PosterUrlStr;
                bitmapImage.UriSource = new Uri(urlStr);
                me.PosterSource = bitmapImage;
            }

            if (this.enableFullWindow.IsChecked.Value)
            {
                me.IsFullWindow = true;
            }

            me.Stretch = GetStretchMode(this.stretchMode.SelectedIndex);
            MediaPanel.Children.Add(me);
            Log.Text = "Legacy made it";
            me.DoubleTapped += this.OnDoubleTapped;
            me.Tapped += this.OnTapped;
            me.TransportControls.DoubleTapped += this.OnDoubleTapped;

            me.UpdateLayout();
            this.UpdateLayout();
        }

        private async void OnCreateMPE(object sender, Windows.UI.Xaml.RoutedEventArgs evargs)
        {
            try
            {
                await InitializeMPE();
            }
            catch (Exception e)
            {
                Log.Text = e.ToString();
            }
        }

        private void OnCreateME(object sender, Windows.UI.Xaml.RoutedEventArgs evargs)
        {
            try
            {
                InitializeME();
            }
            catch (Exception e)
            {
                Log.Text = e.ToString();
            }
        }

        private void OnClear(object sender, Windows.UI.Xaml.RoutedEventArgs evargs)
        {
            var mpe = m as MediaPlayerElement;
            if (mpe != null)
            {
                m = null;
                mpe.Tapped -= OnTapped;
                mpe.PointerPressed -= OnPointerPressed;
                mpe.DoubleTapped -= OnDoubleTapped;
                //if (this.extPlayer.IsChecked.Value)
                {
                    mpe.SetMediaPlayer(null);
                }
                mpe = null;
            }

            var me = m as MediaElement;
            if (me != null)
            {
                m = null;
                me.Tapped -= OnTapped;
                me.PointerPressed -= OnPointerPressed;
                me.DoubleTapped -= OnDoubleTapped;
                me.TransportControls.DoubleTapped -= this.OnDoubleTapped;
                me = null;
            }

            //VideoElementContainer.Content = null;
            MediaPanel.Children.Clear();
            GC.Collect();
            GC.Collect();
            Log.Text = "Clear";
        }

        private void OnCrash(object sender, RoutedEventArgs e)
        {
            var mpe = m as MediaPlayerElement;
            if (mpe != null)
            {
                if (mpe.Source != null)
                {
                    var item = mpe.Source as MediaPlaybackItem;
                    if (item != null && item.Source != null)
                    {
                        item.Source.Dispose();
                    }
                }
            }
        }

        private async void OnUpdateSource(object sender, SelectionChangedEventArgs e)
        {
            var mpe = m as MediaPlayerElement;
            var me = m as MediaElement;
            if (mpe != null)
            {
                if (this.extPlayer.IsChecked.Value)
                {
                    mpe.MediaPlayer.Source = await CreateSource();
                }
                else
                {
                    mpe.Source = await CreateSource();
                }
            }
            else if (me != null)
            {
                var s = CreateSourceME();
                if (s is Uri)
                {
                    me.Source = (Uri)s;
                }
                else
                {
                    me.SetPlaybackSource(s as IMediaPlaybackSource);
                }
            }
        }

        private void zoomButton_Click(object sender, RoutedEventArgs e)
        {
            var mpe = m as MediaPlayerElement;
            if (mpe != null)
            {
                var playbackSession = mpe.MediaPlayer.PlaybackSession;
                var initialRect = new Windows.Foundation.Rect()
                {
                    X = 0.25,
                    Y = 0.25,
                    Height = 0.75,
                    Width = 0.75,
                };
                playbackSession.NormalizedSourceRect = initialRect;

                /*
                var mpp = mpe.FindElementOfTypeInSubtree<MediaPlayerPresenter>();
                mpp.RenderTransformOrigin = new Windows.Foundation.Point(0.5, 0.5);
                mpp.RenderTransform = new ScaleTransform()
                {
                    ScaleX = 0.5,
                    ScaleY = 0.5,
                    CenterX = 0.5,
                    CenterY = 0.5,
                };

                mpp.Height = mpp.ActualHeight * 2.0;
                mpp.Width = mpp.ActualWidth * 2.0;
                */
            }
        }

        private async void OnFoo(object sender, RoutedEventArgs e)
        {
            using (var bitmap = await this.CaptureVisualAsync())
            {
                await bitmap.LogAsync("Test");
            }

            Size bmpSize = this.RenderSize;
            Windows.Graphics.Imaging.SoftwareBitmap swBmp = new Windows.Graphics.Imaging.SoftwareBitmap(Windows.Graphics.Imaging.BitmapPixelFormat.Bgra8, ((int)bmpSize.Width), ((int)bmpSize.Height), Windows.Graphics.Imaging.BitmapAlphaMode.Premultiplied);

            var renderTgtBmp = new RenderTargetBitmap();
            await renderTgtBmp.RenderAsync(this);
            await SaveBitmap(renderTgtBmp, swBmp);
        }

        private async Task SaveBitmap(RenderTargetBitmap rtb, SoftwareBitmap swBmp)
        {
            FileSavePicker filePicker = new Windows.Storage.Pickers.FileSavePicker();
            filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.FileTypeChoices.Add("JPG file", new List<string>() { ".jpg" });
            StorageFile file = await filePicker.PickSaveFileAsync();
            if (file != null)
            {
                using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    IBuffer buffer = await rtb.GetPixelsAsync();
                    Windows.Storage.Streams.DataReader dataReader = Windows.Storage.Streams.DataReader.FromBuffer(buffer);
                    byte[] generatedImage = new byte[buffer.Length];
                    dataReader.ReadBytes(generatedImage);
                    var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.BmpEncoderId, stream);
                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied, (uint)rtb.PixelWidth, (uint)rtb.PixelHeight, swBmp.DpiX, swBmp.DpiY, generatedImage);
                    await encoder.FlushAsync();
                }
            }
        }


        private void OnToggleLayout(object sender, RoutedEventArgs e)
        {
            if (mediaRow.Height == GridLength.Auto)
            {
                mediaRow.Height = new GridLength(1, GridUnitType.Star);
                mediaCol.Width = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                mediaRow.Height = GridLength.Auto;
                mediaCol.Width = GridLength.Auto;
            }
        }
    }
}
