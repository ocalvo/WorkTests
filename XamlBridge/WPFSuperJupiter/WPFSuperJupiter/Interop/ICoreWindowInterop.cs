using Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace Interop
{
    public class ICoreWindowInterop : IInspectable
    {
        private delegate UInt32 Fn_ICoreWindowInterop_get_WindowHandle(IntPtr thisPtr, out IntPtr hwnd);

        private Fn_ICoreWindowInterop_get_WindowHandle ICoreWindowInterop_get_WindowHandle { get { return GetDelegateForVirtualFunction<Fn_ICoreWindowInterop_get_WindowHandle>(3); } }

        private static IntPtr GetPtrFromCoreWindow(CoreWindow coreWindow)
        {
            IInspectable inspectable = new IInspectable(Marshal.GetIUnknownForObject(coreWindow));

            IntPtr ptr = inspectable.QueryInterface(new Guid("45D64A29-A63E-4CB6-B498-5781D298CB4F"));

            inspectable.Dispose();

            return ptr;
        }

        public ICoreWindowInterop(CoreWindow coreWindow) :
            base(GetPtrFromCoreWindow(coreWindow))
        {
        }

        public IntPtr GetWindowHandle()
        {
            IntPtr hwnd;
            UInt32 hr = ICoreWindowInterop_get_WindowHandle(_ptr, out hwnd);

            if (0 != hr)
            {
                throw new Exception(String.Format("ICoreWindowInterop::get_WindowHandle() failed with 0x{0:X}", hr));
            }

            return hwnd;
        }
    }
}