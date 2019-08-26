using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls.Primitives;
using System;
using Windows.UI.Xaml.Media;

namespace SuperJupiter.Views
{

    public sealed partial class FocusEngagementView : Page
    {
        public FocusEngagementView()
        {
            this.InitializeComponent();

            Slider1.IsFocusEngagementEnabled = true;
            Slider2.IsFocusEngagementEnabled = true;
            gv1.IsFocusEngagementEnabled = true;
            combobox1.IsFocusEngagementEnabled = true;
        }

        private void GiveCombobox2Focus(object sender, RoutedEventArgs e)
        {
            combobox2.Focus(FocusState.Keyboard);
        }

    }


}
