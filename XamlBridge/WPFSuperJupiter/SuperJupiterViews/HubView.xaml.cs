using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class HubView : Page
    {
        public HubView()
        {
            this.InitializeComponent();
        }

        private async void Hub_SectionHeaderClick(object sender, HubSectionHeaderClickEventArgs e)
        {
            interactiveHeader.Header = e.Section.Name;
            switch (e.Section.Name)
            {
                case "interactiveHeader":
                    var messageDialog = new MessageDialog("Oh wow! You clicked on an interactive header that does nothing.  Big Deal!");

                    // Show the message dialog and wait

                    messageDialog.Commands.Add(new UICommand("Yes"));
                    messageDialog.Commands.Add(new UICommand("Yes"));

                    await messageDialog.ShowAsync();
                    break;
                default:
                    break;
            }
        }
    }
}
