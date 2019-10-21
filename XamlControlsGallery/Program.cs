using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AppUIBasics
{
    public static class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr LoadLibrary(string lpFileName);

        public static void Main(string[] args)
        {
            //var k = new Microsoft.UI.Xaml.XamlTypeInfo.XamlControlsXamlMetaDataProvider();

            Microsoft.UI.Xaml.Application.Start((p) => new App());
        }
    }
}
