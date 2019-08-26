using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SuperJupiter.Views
{
    public class MyDataObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _value;

        public MyDataObject()
        {
            _value = 1;
        }

        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                }
            }
        }
    }

    public sealed partial class BindingTestView : Page
    {
        public BindingTestView()
        {
            this.InitializeComponent();
            DataContext = new MyDataObject();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            ((MyDataObject)DataContext).Value++;
        }
    }
}
