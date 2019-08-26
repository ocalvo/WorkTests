using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace SuperJupiter.Views
{
    public sealed partial class FocusedElementRemovedView : Page
    {
        private bool isDeleted;
        private bool isDeletedB;

        public FocusedElementRemovedView()
        {
            this.InitializeComponent();

            isDeleted = false;
            isDeletedB = false;

            //FocusManagerPrivateAPIs.Subscribe(deleteMe, deleteMeB, focusButtonA, focusButtonB);
        }

        private void DeleteMe(object sender, RoutedEventArgs e)
        {
            sp.Children.Remove((Button)sender);

            if(((Button)sender) == deleteMe)
            {
                isDeleted = true;
            }
            else
            {
                isDeletedB = true;
            }
        }

        private void Recreate(object sender, RoutedEventArgs e)
        {
            if(((Button)sender) == focusButtonA)
            {
                if (isDeleted)
                {
                    sp.Children.Add(deleteMe);
                    isDeleted = false;
                }
            }
            else
            {
                if (isDeletedB)
                {
                    sp.Children.Add(deleteMeB);
                    isDeletedB = false;
                }
            }
            
        }
    }
}
