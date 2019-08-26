using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace Interop
{
    public class XamlBridgeStatics : IInspectable
    {
        private delegate UInt32 Fn_XamlBridgeStatics_CreateForParentHwnd(IntPtr thisPtr, int parentHwnd, out int hostHwnd, out IntPtr xamlBridge);

        private Fn_XamlBridgeStatics_CreateForParentHwnd XamlBridgeStatics_CreateForParentHwnd { get { return GetDelegateForVirtualFunction<Fn_XamlBridgeStatics_CreateForParentHwnd>(6); } }

        public XamlBridgeStatics() :
            base(WinRTInterop.GetActivationFactory("Windows.UI.Xaml.Hosting.XamlBridge", new Guid("07d3aa03-50a9-4891-9144-80c08f864278")))
        {
        }

        public XamlBridge CreateForParentHwnd(int parentHwnd, out int hostHwnd)
        {
            IntPtr xamlBridge;
            UInt32 hr = XamlBridgeStatics_CreateForParentHwnd(_ptr, parentHwnd, out hostHwnd, out xamlBridge);

            if (0 != hr)
            {
                throw new Exception(String.Format("XamlBridgeStatics::CreateForParentHwnd({0}) failed with 0x{1:X}", parentHwnd, hr));
            }

            return new XamlBridge(xamlBridge);
        }
    }

    public class XamlBridge : IInspectable
    {
        private delegate UInt32 Fn_XamlBridge_InitializeBridge(IntPtr thisPtr);

        private Fn_XamlBridge_InitializeBridge XamlBridge_InitializeBridge { get { return GetDelegateForVirtualFunction<Fn_XamlBridge_InitializeBridge>(8); } }

        public XamlBridge(IntPtr ptr) : base(ptr)
        {
        }

        public void InitializeBridge()
        {
            UInt32 hr = XamlBridge_InitializeBridge(_ptr);

            if (0 != hr)
            {
                throw new Exception(String.Format("XamlBridge::InitializeBridge() failed with 0x{0:X}", hr));
            }
        }
    }
}
