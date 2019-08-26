using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class ButtonBaseView : Page
    {
        public ButtonBaseView()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClickMessage.Text = "Click Message: You clicked the button.";
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            ClickMessage2.Text = "Click Message: You clicked the hyperlink button.";
        }
    }
}