using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class SemanticZoomView : Page
    {
        MockData mockData = null;
        List<GroupInfoList<object>> dataLetter;
        public SemanticZoomView()
        {
            this.InitializeComponent();

            // creates a new instance of the sample data
            mockData = new MockData();

            // sets the list of categories to the groups from the sample data
            dataLetter = mockData.GetGroupsByLetter();
            // sets the CollectionViewSource in the XAML page resources to the data groups
            cvs.Source = dataLetter;
            // sets the items source for the zoomed out view to the group data as well
            (semanticZoom.ZoomedOutView as ListViewBase).ItemsSource = cvs.View.CollectionGroups;
        }
    }

    public class MockData
    {
        List<string> Names = new List<string>()
        {
            "Aaron",
            "Abbey",
            "Abbie",

            "Bailey",
            "Bambi",
            "Bao",
            "Barabara",

            "Caleb",
            "Calista",

            "Daisey",
            "Daisy",

            "Eda",
            "Edda",
        };

        public MockData() { }

        internal List<GroupInfoList<object>> GetGroupsByLetter()
        {
            List<GroupInfoList<object>> groups = new List<GroupInfoList<object>>();

            var query = from item in Names
                        orderby item
                        group item by item[0] into g
                        select new { GroupName = g.Key.ToString().ToLower(), Items = g };
            foreach (var g in query)
            {
                GroupInfoList<object> info = new GroupInfoList<object>();
                info.Key = g.GroupName;
                foreach (var item in g.Items)
                {
                    info.Add(item);
                }
                groups.Add(info);
            }

            return groups;
        }

    }

    public class GroupInfoList<T> : List<object>
    {

        public object Key { get; set; }


        public new IEnumerator<object> GetEnumerator()
        {
            return (System.Collections.Generic.IEnumerator<object>)base.GetEnumerator();
        }
    }
}
