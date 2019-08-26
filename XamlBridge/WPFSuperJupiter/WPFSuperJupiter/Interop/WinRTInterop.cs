using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Interop
{
    public static class ComBase
    {
        [DllImport("combase.dll")]
        public static extern UInt32 RoGetActivationFactory(IntPtr activatableClassId, [In] ref Guid iid, out IntPtr factory);

        [DllImport("combase.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern void WindowsCreateString(String sourceString, UInt32 length, out IntPtr hstring);

        [DllImport("combase.dll")]
        public static extern UInt32 WindowsDeleteString(IntPtr hstring);
    }

    public static class WinRTInterop
    {
        private static IntPtr CreateHString(String source)
        {
            IntPtr hstring;
            ComBase.WindowsCreateString(source, (UInt32)source.Length, out hstring);
            return hstring;
        }

        public static IntPtr GetActivationFactory(String activatableClassId, Guid iid)
        {
            IntPtr hstring = CreateHString(activatableClassId);
            IntPtr factory;
            UInt32 hr = ComBase.RoGetActivationFactory(hstring, ref iid, out factory);

            if (0 != hr)
            {
                String message = String.Format("RoGetActivationFactory({0}, {1}) failed with 0x{2:X}", activatableClassId, iid, hr);
                throw new Exception(message);
            }

            ComBase.WindowsDeleteString(hstring);

            return factory;
        }
    }
}
