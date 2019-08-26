using System;
using System.IO;
using System.Reflection;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SuperJupiter.Views
{
    public sealed partial class MediaView : Page
    {
        private TimedTextSource _srtTimedTextSource;
        
        public MediaView()
        {
            this.InitializeComponent();

            String mediaFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", "jellies.mp4");
            String textSourceFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", "jellies.srt");

            String mediaFileUri = "file:///" + mediaFilePath.Replace('\\', '/');
            String textSourceFileUri = "file:///" + textSourceFilePath.Replace('\\', '/');

            var source = MediaSource.CreateFromUri(new Uri(mediaFileUri));
            
            _srtTimedTextSource = TimedTextSource.CreateFromUri(new Uri(textSourceFileUri));
            _srtTimedTextSource.Resolved += Tts_Resolved;
            
            source.ExternalTimedTextSources.Add(_srtTimedTextSource);

            this.mediaElement.SetPlaybackSource(new MediaPlaybackItem(source));
        }
        
        private void Tts_Resolved(TimedTextSource sender, TimedTextSourceResolveResultEventArgs args)
        {
            if (sender == _srtTimedTextSource)
            {
                args.Tracks[0].Label = "English SRT";
            }
        }
    }
}
