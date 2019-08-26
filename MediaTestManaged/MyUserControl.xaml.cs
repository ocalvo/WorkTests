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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MediaTestManaged
{
    public sealed partial class MyUserControl : UserControl
    {
        public MyUserControl()
        {
            this.InitializeComponent();
        }

        protected async override void OnDoubleTapped(DoubleTappedRoutedEventArgs e)
        {
            scaleTransform.ScaleX -= 0.1;
            scaleTransform.ScaleY -= 0.1;
            base.OnDoubleTapped(e);
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => { });
        }
    }
}
