using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace SuperJupiter.Views
{
    public sealed partial class FromToByAnimationsView : Page
    {
        private static readonly int seekTime1 = 0;
        private static readonly int seekTime2 = 2;
        private static readonly int seekTime3 = 4;

        TimeSpan animationDuration = TimeSpan.FromSeconds(3);
        Storyboard mainStoryboard;

        // The following are used for verifying animations that only have their FROM property set
        ColorAnimation colorAnimation_From;
        DoubleAnimation doubleAnimation_From;
        PointAnimation pointAnimation_From;

        // The following are used for verifying animations that have their FROM and TO properties set
        ColorAnimation colorAnimation_FromTo;
        DoubleAnimation doubleAnimation_FromTo;
        PointAnimation pointAnimation_FromTo;

        // The following are used for verifying animations that have their FROM and BY properties set
        ColorAnimation colorAnimation_FromBy;
        DoubleAnimation doubleAnimation_FromBy;
        PointAnimation pointAnimation_FromBy;

        // The following are used for verifying animations that have their TO and BY properties set
        ColorAnimation colorAnimation_ToBy;
        DoubleAnimation doubleAnimation_ToBy;
        PointAnimation pointAnimation_ToBy;

        // The following are used for verifying animations that only have their TO property set
        ColorAnimation colorAnimation_To;
        DoubleAnimation doubleAnimation_To;
        PointAnimation pointAnimation_To;

        // The following are used for verifying animations that only have their BY property set
        ColorAnimation colorAnimation_By;
        DoubleAnimation doubleAnimation_By;
        PointAnimation pointAnimation_By;

        public FromToByAnimationsView()
        {
            this.InitializeComponent();
            this.Loaded += FromToBy_TestPage_Loaded;
        }

        void FromToBy_TestPage_Loaded(object sender, RoutedEventArgs e)
        {
            SetupScene();
        }

        private void Seek1_Click(object sender, RoutedEventArgs e)
        {
            // stop the animation if it is currently playing and then seek to the specific time
            this.mainStoryboard.Stop();
            this.mainStoryboard.Begin();
            this.mainStoryboard.Pause();
            SeekToSeconds(seekTime1);
        }

        private void Seek2_Click(object sender, RoutedEventArgs e)
        {
            // stop the animation if it is currently playing and then seek to the specific time
            this.mainStoryboard.Stop();
            this.mainStoryboard.Begin();
            this.mainStoryboard.Pause();
            SeekToSeconds(seekTime2);
        }

        private void Seek3_Click(object sender, RoutedEventArgs e)
        {
            // stop the animation if it is currently playing and then seek to the specific time
            this.mainStoryboard.Stop();
            this.mainStoryboard.Begin();
            this.mainStoryboard.Pause();
            SeekToSeconds(seekTime3);
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            // restart the animation by calling Stop() and then Begin()
            this.mainStoryboard.Stop();
            this.mainStoryboard.Begin();
        }

        private void SetupScene()
        {
            mainStoryboard = new Storyboard();

            // Create animation objects and add them to the main storyboard
            colorAnimation_From = CreateColorAnimation(ColorAnimationTarget_From, animationDuration);
            doubleAnimation_From = CreateDoubleAnimation(DoubleAnimationTarget_From, animationDuration);
            pointAnimation_From = CreatePointAnimation(PointAnimationTarget_From, animationDuration);
            mainStoryboard.Children.Add(colorAnimation_From);
            mainStoryboard.Children.Add(doubleAnimation_From);
            mainStoryboard.Children.Add(pointAnimation_From);

            colorAnimation_FromTo = CreateColorAnimation(ColorAnimationTarget_FromTo, animationDuration);
            doubleAnimation_FromTo = CreateDoubleAnimation(DoubleAnimationTarget_FromTo, animationDuration);
            pointAnimation_FromTo = CreatePointAnimation(PointAnimationTarget_FromTo, animationDuration);
            mainStoryboard.Children.Add(colorAnimation_FromTo);
            mainStoryboard.Children.Add(doubleAnimation_FromTo);
            mainStoryboard.Children.Add(pointAnimation_FromTo);

            colorAnimation_FromBy = CreateColorAnimation(ColorAnimationTarget_FromBy, animationDuration);
            doubleAnimation_FromBy = CreateDoubleAnimation(DoubleAnimationTarget_FromBy, animationDuration);
            pointAnimation_FromBy = CreatePointAnimation(PointAnimationTarget_FromBy, animationDuration);
            mainStoryboard.Children.Add(colorAnimation_FromBy);
            mainStoryboard.Children.Add(doubleAnimation_FromBy);
            mainStoryboard.Children.Add(pointAnimation_FromBy);

            colorAnimation_ToBy = CreateColorAnimation(ColorAnimationTarget_ToBy, animationDuration);
            doubleAnimation_ToBy = CreateDoubleAnimation(DoubleAnimationTarget_ToBy, animationDuration);
            pointAnimation_ToBy = CreatePointAnimation(PointAnimationTarget_ToBy, animationDuration);
            mainStoryboard.Children.Add(colorAnimation_ToBy);
            mainStoryboard.Children.Add(doubleAnimation_ToBy);
            mainStoryboard.Children.Add(pointAnimation_ToBy);

            colorAnimation_To = CreateColorAnimation(ColorAnimationTarget_To, animationDuration);
            doubleAnimation_To = CreateDoubleAnimation(DoubleAnimationTarget_To, animationDuration);
            pointAnimation_To = CreatePointAnimation(PointAnimationTarget_To, animationDuration);
            mainStoryboard.Children.Add(colorAnimation_To);
            mainStoryboard.Children.Add(doubleAnimation_To);
            mainStoryboard.Children.Add(pointAnimation_To);

            colorAnimation_By = CreateColorAnimation(ColorAnimationTarget_By, animationDuration);
            doubleAnimation_By = CreateDoubleAnimation(DoubleAnimationTarget_By, animationDuration);
            pointAnimation_By = CreatePointAnimation(PointAnimationTarget_By, animationDuration);
            mainStoryboard.Children.Add(colorAnimation_By);
            mainStoryboard.Children.Add(doubleAnimation_By);
            mainStoryboard.Children.Add(pointAnimation_By);

            // set the proper FROM,TO,BY values
            colorAnimation_From.From = Colors.Pink;
            doubleAnimation_From.From = -50;
            pointAnimation_From.From = new Windows.Foundation.Point(-30, 110);

            colorAnimation_FromTo.From = Colors.Pink;
            colorAnimation_FromTo.To = Colors.Cyan;
            doubleAnimation_FromTo.From = -50;
            doubleAnimation_FromTo.To = 50;
            pointAnimation_FromTo.From = new Windows.Foundation.Point(-30, 270);
            pointAnimation_FromTo.To = new Windows.Foundation.Point(70, 270);

            colorAnimation_FromBy.From = Colors.Purple;
            colorAnimation_FromBy.By = Colors.Yellow;
            doubleAnimation_FromBy.From = -50;
            doubleAnimation_FromBy.By = 100;
            pointAnimation_FromBy.From = new Windows.Foundation.Point(-30, 430);
            pointAnimation_FromBy.By = new Windows.Foundation.Point(100, 0);

            colorAnimation_ToBy.To = Colors.Cyan;
            colorAnimation_ToBy.By = Colors.Yellow;
            doubleAnimation_ToBy.To = 200;
            doubleAnimation_ToBy.By = 100;
            pointAnimation_ToBy.To = new Windows.Foundation.Point(220, 110);
            pointAnimation_ToBy.By = new Windows.Foundation.Point(150, 0);

            colorAnimation_To.To = Colors.Pink;
            doubleAnimation_To.To = 200;
            pointAnimation_To.To = new Windows.Foundation.Point(220, 270);

            colorAnimation_By.By = Colors.Brown;
            doubleAnimation_By.By = 40;
            pointAnimation_By.By = new Windows.Foundation.Point(40, 0);

            // Begin the animation
            mainStoryboard.Begin();
        }

        private ColorAnimation CreateColorAnimation(DependencyObject target, Duration duration)
        {
            ColorAnimation animation = new ColorAnimation() { Duration = duration };
            Storyboard.SetTarget(animation, target);
            Storyboard.SetTargetProperty(animation, "(Rectangle.Fill).(SolidColorBrush.Color)");
            return animation;
        }

        private DoubleAnimation CreateDoubleAnimation(DependencyObject target, Duration duration)
        {
            DoubleAnimation animation = new DoubleAnimation() { Duration = duration };
            Storyboard.SetTarget(animation, target);
            Storyboard.SetTargetProperty(animation, "(Canvas.Left)");
            return animation;
        }

        private PointAnimation CreatePointAnimation(DependencyObject target, Duration duration)
        {
            PointAnimation animation = new PointAnimation() { Duration = duration, EnableDependentAnimation = true };
            Storyboard.SetTarget(animation, target);
            Storyboard.SetTargetProperty(animation, "Center");
            return animation;
        }

        private void SeekToSeconds(int timeToSeekTo)
        {
            mainStoryboard.Seek(TimeSpan.FromSeconds(timeToSeekTo));
        }
    }
}
