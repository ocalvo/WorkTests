using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class MessageDialogView : Page
    {
        public MessageDialogView()
        {
            this.InitializeComponent();
        }

        private async void messageBoxClick(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("'All the single ladies, say Bello!'");

            // Show the message dialog and wait

            messageDialog.Commands.Add(new UICommand("Bello"));
            messageDialog.Commands.Add(new UICommand("Noop"));

            await messageDialog.ShowAsync();
        }
    }
}