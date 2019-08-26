using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace XamlClassLibrary
{
    public sealed partial class TestUserControl : UserControl
    {
        public TestUserControl()
        {
            this.InitializeComponent();

            inkcanvas.InkPresenter.InputDeviceTypes = Windows.UI.Core.CoreInputDeviceTypes.Pen | Windows.UI.Core.CoreInputDeviceTypes.Mouse | Windows.UI.Core.CoreInputDeviceTypes.Touch;
            InkDrawingAttributes attributes = new InkDrawingAttributes();
            attributes.Color = Colors.White;
            inkcanvas.InkPresenter.UpdateDefaultDrawingAttributes(attributes);

            String mediaFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "MicrosoftLogo.mp4");
            String mediaFileUri = "file:///" + mediaFilePath.Replace('\\', '/');
            MediaSource mediaSource = MediaSource.CreateFromUri(new Uri(mediaFileUri));

            mediaplayerelement.Source = new MediaPlaybackItem(mediaSource);
            mediaplayerelement.MediaPlayer.IsLoopingEnabled = true;
        }
    }
}
