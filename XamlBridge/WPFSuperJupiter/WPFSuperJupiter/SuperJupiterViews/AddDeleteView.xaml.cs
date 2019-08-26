using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace SuperJupiter.Views
{
    public sealed partial class AddDeleteView : Page
    {
        Random rand = new Random(1000); // use a specific seed to keep it deterministic

        public AddDeleteView()
        {
            this.InitializeComponent();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (rectangleItems.Items.Count > 0)
                rectangleItems.Items.RemoveAt(0);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Rectangle newItem = new Rectangle();

            newItem.Height = 50;
            newItem.Width = 50;
            newItem.Margin = new Thickness(5);
            newItem.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255)));

            // Add a new Rectangle of a random color.
            rectangleItems.Items.Add(newItem);
        }
    }
}
