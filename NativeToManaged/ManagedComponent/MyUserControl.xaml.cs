using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SampleLibraryCS
{
    public sealed partial class MyUserControl : UserControl
    {
        public MyUserControl()
        {
            try
            {
                this.InitializeComponent();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public MyUserControl(int p)
        {

        }

    }
}
