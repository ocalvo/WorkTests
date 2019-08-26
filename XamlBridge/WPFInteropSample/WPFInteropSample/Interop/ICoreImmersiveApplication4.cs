using Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;

namespace Interop
{
    public class ICoreApplicationPrivate2 : IInspectable
    {
        private delegate UInt32 Fn_ICoreApplicationPrivate2_CreateNonImmersiveView(IntPtr thisPtr, out IntPtr view);

        private Fn_ICoreApplicationPrivate2_CreateNonImmersiveView ICoreApplicationPrivate2_CreateNonImmersiveView { get { return GetDelegateForVirtualFunction<Fn_ICoreApplicationPrivate2_CreateNonImmersiveView>(8); } }

        public ICoreApplicationPrivate2() :
            base(WinRTInterop.GetActivationFactory("Windows.ApplicationModel.Core.CoreApplication", new Guid("6090202D-2843-4BA5-9B0D-FC88EECD9CE5")))
        {
        }

        public CoreApplicationView CreateNonImmersiveView()
        {
            IntPtr view;
            UInt32 hr = ICoreApplicationPrivate2_CreateNonImmersiveView(_ptr, out view);

            if (0 != hr)
            {
                throw new Exception(String.Format("ICoreApplicationPrivate2::CreateNonImmersiveView() failed with 0x{0:X}", hr));
            }

            IInspectable inspectable = new IInspectable(view);

            CoreApplicationView coreApplicationView = (CoreApplicationView)Marshal.GetObjectForIUnknown(view);

            inspectable.Dispose();

            return coreApplicationView;
        }
    }
}