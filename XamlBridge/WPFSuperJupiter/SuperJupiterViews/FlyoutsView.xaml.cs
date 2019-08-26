using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace SuperJupiter.Views
{
    public sealed partial class FlyoutsView : Page
    {
        public FlyoutsView()
        {
            this.InitializeComponent();
            Button1.DataContext = this;
        }

        private void Element_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                FlyoutBase.ShowAttachedFlyout(element);
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            IsShuffleEnabled = false;
            IsRepeatEnabled = false;
        }

        public static readonly DependencyProperty IsShuffleEnabledProperty =
            DependencyProperty.Register(
                "IsShuffleEnabled", typeof(Boolean),
                typeof(Page), null
            );

        public bool IsShuffleEnabled
        {
            get { return (bool)GetValue(IsShuffleEnabledProperty); }
            set { SetValue(IsShuffleEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsRepeatEnabledProperty =
            DependencyProperty.Register(
                "IsRepeatEnabled", typeof(Boolean),
                typeof(Page), null
            );

        public bool IsRepeatEnabled
        {
            get { return (bool)GetValue(IsRepeatEnabledProperty); }
            set { SetValue(IsRepeatEnabledProperty, value); }
        }
    }
}
