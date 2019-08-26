using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace Interop
{

    public class XamlPresenterStatics : IInspectable
    {
        private delegate UInt32 Fn_XamlPresenterStatics_CreateFromHwnd(IntPtr thisPtr, int hwnd, out IntPtr xamlPresenter);

        private Fn_XamlPresenterStatics_CreateFromHwnd XamlPresenterStatics_CreateFromHwnd { get { return GetDelegateForVirtualFunction<Fn_XamlPresenterStatics_CreateFromHwnd>(6); } }

        public XamlPresenterStatics() :
            base(WinRTInterop.GetActivationFactory("Windows.UI.Xaml.Hosting.XamlPresenter", new Guid("5c6ef05e-f60d-4433-8bc6-40586456afeb")))
        {
        }

        public XamlPresenter CreateFromHwnd(int hwnd)
        {
            IntPtr xamlPresenter;
            UInt32 hr = XamlPresenterStatics_CreateFromHwnd(_ptr, hwnd, out xamlPresenter);

            if (0 != hr)
            {
                throw new Exception(String.Format("XamlPresenterStatics::CreateFromHwnd({0}) failed with 0x{1:X}", hwnd, hr));
            }

            return new XamlPresenter(xamlPresenter);
        }
    }

    public class XamlPresenterStatics3 : IInspectable
    {
        private delegate UInt32 Fn_XamlPresenterStatics3_CreateFromCoreWindow(IntPtr thisPtr, IntPtr pCoreWindow, out IntPtr xamlPresenter);

        private Fn_XamlPresenterStatics3_CreateFromCoreWindow XamlPresenterStatics3_CreateFromCoreWindow { get { return GetDelegateForVirtualFunction<Fn_XamlPresenterStatics3_CreateFromCoreWindow>(6); } }

        public XamlPresenterStatics3() :
            base(WinRTInterop.GetActivationFactory("Windows.UI.Xaml.Hosting.XamlPresenter", new Guid("a49dea01-9e75-49f0-beee-ef1592fbc82b")))
        {
        }

        public XamlPresenter CreateFromCoreWindow(CoreWindow coreWindow)
        {
            IInspectable pCoreWindow = new IInspectable(Marshal.GetComInterfaceForObject<CoreWindow, ICoreWindow>(coreWindow));

            IntPtr xamlPresenter;
            UInt32 hr = XamlPresenterStatics3_CreateFromCoreWindow(_ptr, pCoreWindow.Value, out xamlPresenter);
            pCoreWindow.Dispose();

            if (0 != hr)
            {
                throw new Exception(String.Format("XamlPresenterStatics3::CreateFromCoreWindow({0x{0:X}}) failed with 0x{1:X}", pCoreWindow, hr));
            }

            return new XamlPresenter(xamlPresenter);
        }
    }


    public class XamlPresenter : IInspectable
    {
        private delegate UInt32 Fn_XamlPresenter_InitializePresenter(IntPtr thisPtr);

        private Fn_XamlPresenter_InitializePresenter XamlPresenter_InitializePresenter { get { return GetDelegateForVirtualFunction<Fn_XamlPresenter_InitializePresenter>(9); } }

        public XamlPresenter(IntPtr ptr) : base(ptr)
        {
        }

        public void InitializePresenter()
        {
            UInt32 hr = XamlPresenter_InitializePresenter(_ptr);

            if (0 != hr)
            {
                throw new Exception(String.Format("XamlPresenter::InitializePresenter() failed with 0x{0:X}", hr));
            }
        }
    }
}
