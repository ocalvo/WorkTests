using SuperJupiter.Shared;
using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class ListViewBaseView : Page
    {
        private GeneratorIncrementalLoadingClass<SuperJupiter.Shared.SampleListItems> items;

        public ListViewBaseView()
        {
            this.InitializeComponent();
            items = new GeneratorIncrementalLoadingClass<SuperJupiter.Shared.SampleListItems>(30, (count) => { return new SuperJupiter.Shared.SampleListItems { Name = ("Item number: ") + count }; });
            ItemsList.Source = items;
        }
    }
}

