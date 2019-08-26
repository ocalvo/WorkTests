using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace SuperJupiter.Views
{
    public sealed partial class RepositionThemeTransitionView : Page
    {
        Random rand = new Random(1000); // use a specific seed to keep it deterministic

        public RepositionThemeTransitionView()
        {
            this.InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Rectangle newItem = new Rectangle();

            newItem.Height = 50;
            newItem.Width = 50;
            newItem.Margin = new Thickness(5);
            newItem.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255)));

            int idx = 0;
            if (rectangleItems.Items.Count > 0)
            {
                idx = rand.Next(0, rectangleItems.Items.Count);
            }

            // Insert a new Rectangle of a random color into the ItemsControl at random location in the list.
            rectangleItems.Items.Insert(idx, newItem);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (rectangleItems.Items.Count > 0)
            {
                // Remove a Rectangle from a random location in the list.
                int idx = rand.Next(0, rectangleItems.Items.Count);
                rectangleItems.Items.RemoveAt(idx);
            }
        }
    }
}

