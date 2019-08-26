using Private.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFInteropSample.Test
{
    public sealed class TestTest: Windows.UI.Xaml.Tests.Common.XamlTestsBase
    {
        TestTest()
        {
            TestServices.EnsureInitialized();
        }
    }
}
