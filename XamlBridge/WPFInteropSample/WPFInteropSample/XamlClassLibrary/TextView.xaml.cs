using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class TextView : Page
    {
        public TextView()
        {
            this.InitializeComponent();
        }

        private void changeLineStacking(object sender, RoutedEventArgs e)
        {
            RadioButton button = sender as RadioButton;

            switch (button.Content.ToString())
            {
                case "BaselineToBaseline":
                    baselineToBaselineTextBlock.LineStackingStrategy = LineStackingStrategy.BaselineToBaseline;
                    break;
                case "BlockLineHeight":
                    baselineToBaselineTextBlock.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
                    break;
                case "MaxHeight":
                    baselineToBaselineTextBlock.LineStackingStrategy = LineStackingStrategy.MaxHeight;
                    break;
            }
        }

        private void toggleVisibility(object sender, RoutedEventArgs e)
        {
            Button send = sender as Button;
            if (send.Content.ToString() == "Super and subscript v") subSuperScript.Visibility = subSuperScript.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            else if (send.Content.ToString() == "Fractions v") fractions.Visibility = fractions.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            else if (send.Content.ToString() == "Historical forms v") historicalForms.Visibility = historicalForms.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            else if(send.Content.ToString() == "Stylistic sets v") stylisticSets.Visibility = stylisticSets.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            else if (send.Content.ToString() == "Ligatures v") Ligatures.Visibility = Ligatures.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            else if (send.Content.ToString() == "Typography v") typography.Visibility = typography.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            tbHyperlinkClicked.Text = "You clicked the Hyperlink!";
        }

    }
}
