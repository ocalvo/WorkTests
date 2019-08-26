using System;
using System.Runtime.InteropServices;
using Windows.UI.Core;

namespace Interop
{
    public static class WindowsUI
    {
        [DllImport("windows.ui.dll", CharSet = CharSet.Unicode, EntryPoint = "#1500")]
        public static extern UInt32 CreateCoreWindow(
            UInt32 windowType,
            String windowTitle,
            Int32 x,
            Int32 y,
            Int32 width,
            Int32 height,
            UInt32 dwAttributes,
            IntPtr ownerWindow,
            [In] ref Guid iid,
            out IntPtr pvObject);
    }

    public static class CoreWindowInterop
    {
        public static CoreWindow CreateCoreWindow(String windowTitle, int x, int y, int width, int height)
        {
            IntPtr pvObject;
            Guid iid = Marshal.GenerateGuidForType(typeof(ICoreWindow));

            UInt32 hr = WindowsUI.CreateCoreWindow(
                6, /* NOT_IMMERSIVE */
                windowTitle,
                x, y, width, height,
                0,
                IntPtr.Zero,
                ref iid,
                out pvObject);

            if (0 != hr)
            {
                throw new Exception(String.Format("CreateCoreWindow failed with 0x{0:X}", hr));
            }

            IInspectable inspectable = new IInspectable(pvObject);

            CoreWindow coreWindow = (CoreWindow) Marshal.GetObjectForIUnknown(pvObject);

            inspectable.Dispose();

            return coreWindow;
        }
    }
}
