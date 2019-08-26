using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SuperJupiter.Views
{
    public sealed partial class ImageView : Page
    {
        public ImageView()
        {
            this.InitializeComponent();
        }

        private void stretchChange(object sender, RoutedEventArgs e)
        {
            RadioButton button = sender as RadioButton;

            switch ( button.Content.ToString() )
            {
                case "None":
                    stretchImage.Stretch = Stretch.None;
                    break;
                case "Fill":
                    stretchImage.Stretch = Stretch.Fill;
                    break;
                case "Uniform":
                    stretchImage.Stretch = Stretch.Uniform;
                    break;
                case "UniformToFill":
                    stretchImage.Stretch = Stretch.UniformToFill;
                    break;
            }
        }

        private void propChange(object sender, RoutedEventArgs e)
        {
            RadioButton button = sender as RadioButton;

            switch (button.Content.ToString())
            {
                case "Ninegrid":
                    image1.NineGrid = new Thickness(100,10,100,10);
                    break;
                case "Opacity":
                    image1.Opacity = 0.5;
                    break;
            }
        }
    }
}
