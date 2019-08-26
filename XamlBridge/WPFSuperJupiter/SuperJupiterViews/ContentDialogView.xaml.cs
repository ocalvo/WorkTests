using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class ContentDialogView : Page
    {
        ContentDialog noWifiDialog = null;

        public ContentDialogView()
        {
            this.InitializeComponent();
        }

        // Show ContentDialog defined in code (not added to visual tree)
        private async Task WifiConnectionLost()
        {
            try
            {
                Slider slider = null;
                noWifiDialog = new ContentDialog()
                {
                    Title = "No wifi connection",                    
                    Content = new StackPanel()
                    {
                        Children =
                        {
                            new TextBlock()
                            {
                                Text = "Check connection and try again",
                            },
                            (slider = new Slider()
                            {
                            }),
                            new Button()
                            {
                                Content = "Another action",
                            },
                        },
                    },
                    PrimaryButtonText = "Ok",
                    SecondaryButtonText = "Cancel",
                };
                
                await noWifiDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                exOutput.Text = ex.Message;
            }
        }

        private async void showWifiDialog(object sender, RoutedEventArgs e)
        {
            await WifiConnectionLost();
        }

        // Show a ContentDialog defined in Xaml (added to visual tree)
        private async void showXamlDialog(object sender, RoutedEventArgs e)
        {
            termsOfUseContentDialog.MaxWidth = this.ActualWidth;
            ((FrameworkElement) (termsOfUseContentDialog.Content)).Width = this.ActualWidth;
            ContentDialogResult result = await termsOfUseContentDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                // Terms of use were accepted.
            }
            else
            {
                // User pressed Cancel or the back arrow.
                // Terms of use were not accepted.
            }
        }

        private async void ShowTermsOfUseContentDialogButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialogResult result = await termsOfUseContentDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                // Terms of use were accepted.
            }
            else
            {
                // User pressed Cancel or the back arrow.
                // Terms of use were not accepted.
            }
        }

        private void TermsOfUseContentDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            // Ensure that the check box is unchecked each time the dialog opens.
            ConfirmAgeCheckBox.IsChecked = false;
        }

        private void ConfirmAgeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            termsOfUseContentDialog.IsPrimaryButtonEnabled = true;
        }

        private void ConfirmAgeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            termsOfUseContentDialog.IsPrimaryButtonEnabled = false;
        }


    }
}
