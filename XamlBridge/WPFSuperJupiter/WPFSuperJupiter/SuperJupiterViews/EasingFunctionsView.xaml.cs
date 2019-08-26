using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

namespace SuperJupiter.Views
{
    public sealed partial class EasingFunctionsView : Page
    {
        public EasingFunctionsView()
        {
            this.InitializeComponent();
            this.Loaded += EasingFunctions_TestPage_Loaded;
        }

        void EasingFunctions_TestPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            EasingFunctionChanged(null, null);
        }

        private void EasingFunctionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (storyboard == null)
                return;

            storyboard.Stop();

            EasingFunctionBase easingFunction = null;

            // select an easing function based on the user's selection
            ComboBoxItem selectedFunctionItem = FunctionSelector.SelectedItem as ComboBoxItem;
            if (selectedFunctionItem != null)
            {
                switch (selectedFunctionItem.Content.ToString())
                {
                    case "BounceEase":
                        easingFunction = new BounceEase();
                        break;
                    case "CircleEase":
                        easingFunction = new CircleEase();
                        break;
                    case "ExponentialEase":
                        easingFunction = new ExponentialEase();
                        break;
                    case "PowerEase":
                        easingFunction = new PowerEase() { Power = 0.5 };
                        break;
                    default:
                        break;
                }
            }

            // if no valid easing function was specified, let the storyboard stay stopped and do not continue
            if (easingFunction == null)
                return;

            ComboBoxItem selectedEasingModeItem = EasingModeSelector.SelectedItem as ComboBoxItem;
            // select an easing mode based on the user's selection, defaulting to EaseIn if no selection was given
            if (selectedEasingModeItem != null)
            {
                switch (selectedEasingModeItem.Content.ToString())
                {
                    case "EaseOut":
                        easingFunction.EasingMode = EasingMode.EaseOut;
                        break;
                    case "EaseInOut":
                        easingFunction.EasingMode = EasingMode.EaseInOut;
                        break;
                    default:
                        easingFunction.EasingMode = EasingMode.EaseIn;
                        break;
                }
            }

            // plot a graph of the easing function
            PlotEasingFunctionGraph(easingFunction, 0.005);

            RectanglePositionAnimation.EasingFunction = easingFunction;
            //GraphPositionMarkerXAnimation.EasingFunction = easingFunction;
            GraphPositionMarkerYAnimation.EasingFunction = easingFunction;

            storyboard.Begin();
        }

        // Plots a graph of the passed easing function using the given sampling interval on the "Graph" Canvas control 
        private void PlotEasingFunctionGraph(EasingFunctionBase easingFunction, double samplingInterval)
        {
            Graph.Children.Clear();

            Path path = new Path();
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure() { StartPoint = new Point(0, 0) };
            PathSegmentCollection pathSegmentCollection = new PathSegmentCollection();

            // Note that an easing function is just like a regular function that operates on doubles.
            // Here we plot the range of the easing function's output on the y-axis of a graph.
            for (double i = 0; i < 1; i += samplingInterval)
            {
                double x = i * GraphContainer.Width;
                double y = easingFunction.Ease(i) * GraphContainer.Height;

                LineSegment segment = new LineSegment();
                segment.Point = new Point(x, y);
                pathSegmentCollection.Add(segment);
            }

            pathFigure.Segments = pathSegmentCollection;
            pathGeometry.Figures.Add(pathFigure);
            path.Data = pathGeometry;
            path.Stroke = new SolidColorBrush(Colors.Black);
            path.StrokeThickness = 1;

            // Add the path to the Canvas
            Graph.Children.Add(path);
        }
    }
}

