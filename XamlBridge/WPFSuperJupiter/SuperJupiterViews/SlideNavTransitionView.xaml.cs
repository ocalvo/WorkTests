using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class SlideNavTransitionView : Page
    {
        public List<string> Items { get; set; }
        public SlideNavTransitionView()
        {
            Items = new List<string>()
            {
                "List Item Number 1",
                "List Item Number 2",
                "List Item Number 3",
                "List Item Number 4",
                "List Item Number 5",
                "List Item Number 6",
                "List Item Number 7",
                "List Item Number 8",
                "List Item Number 9",
                "List Item Number 10"
            };

            this.InitializeComponent();
        }
    }
}

