using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Interop
{
    [Flags]
    public enum WindowClassStyles
    {
        CS_VREDRAW = 1,
        CS_HREDRAW = 2
    }

    public static class WindowStyles
    {
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_CLIPCHILDREN = 0x02000000;
        public const int WS_CHILD = 0x40000000;
        public const int WS_POPUP = unchecked((int)0x80000000);
    }

    public static class WindowMessages
    {
        public const Int32 WM_GETOBJECT = 0x003d;
        public const Int32 WM_KEYDOWN = 0x0100;
        public const Int32 WM_POINTERDOWN = 0x0246;
    }

    public static class VirtualKeyCodes
    {
        public const Int32 VK_TAB = 0x09;
        public const Int32 VK_SHIFT = 0x10;
    }

    public static class WindowLongIndices
    {
        public const Int32 GWL_STYLE = -16;
    }

    public static class User32
    {
        public delegate IntPtr WndProc(IntPtr hwnd, UInt32 uMsg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WNDCLASSEX
        {
            public UInt32 cbSize;
            public UInt32 style;
            public WndProc lpfnWndProc;
            public Int32 cbClsExtra;
            public Int32 cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public String lpszMenuName;
            public String lpszClassName;
            public IntPtr hIconSm;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern UInt16 RegisterClassEx([In] ref WNDCLASSEX lpwcx);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateWindowEx(
           UInt32 dwExStyle,
           String lpClassName,
           String lpWindowName,
           UInt32 dwStyle,
           int x,
           int y,
           int nWidth,
           int nHeight,
           IntPtr hWndParent,
           IntPtr hMenu,
           IntPtr hInstance,
           IntPtr lpParam);

        [DllImport("user32.dll")]
        public static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool EnableMouseInPointer(bool fEnable);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        public static extern Int32 GetWindowLong(IntPtr hWnd, Int32 nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, Int32 nIndex);

        [DllImport("user32.dll")]
        public static extern Int32 SetWindowLong(IntPtr hWnd, Int32 nIndex, Int32 dwNewLong);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, Int32 nIndex, IntPtr dwNewLong);
    }

    public static class Win32Interop
    {
        public static Int32 GetWindowLong(IntPtr hwnd, int nIndex)
        {
            if (IntPtr.Size == 4)
            {
                return User32.GetWindowLong(hwnd, nIndex);
            }
            else
            {
                IntPtr ptr = User32.GetWindowLongPtr(hwnd, nIndex);
                return unchecked((int)ptr.ToInt64());
            }
        }

        public static IntPtr GetWindowLongPtr(IntPtr hwnd, int nIndex)
        {
            if (IntPtr.Size == 4)
            {
                Int32 val = User32.GetWindowLong(hwnd, nIndex);
                return new IntPtr(val);
            }
            else
            {
                return User32.GetWindowLongPtr(hwnd, nIndex);
            }
        }

        public static Int32 SetWindowLong(IntPtr hwnd, int nIndex, Int32 value)
        {
            if (IntPtr.Size == 4)
            {
                return User32.SetWindowLong(hwnd, nIndex, value);
            }
            else
            {
                IntPtr ptr = User32.SetWindowLongPtr(hwnd, nIndex, new IntPtr(value));
                return unchecked((int)ptr.ToInt64());
            }
        }

        public static IntPtr SetWindowLongPtr(IntPtr hwnd, int nIndex, IntPtr value)
        {
            if (IntPtr.Size == 4)
            {
                Int32 val = User32.SetWindowLong(hwnd, nIndex, unchecked((int)value.ToInt64()));
                return new IntPtr(val);
            }
            else
            {
                return User32.SetWindowLongPtr(hwnd, nIndex, value);
            }
        }

        public static Int32 GetWindowStyle(IntPtr hwnd)
        {
            return GetWindowLong(hwnd, WindowLongIndices.GWL_STYLE);
        }

        public static void SetWindowStyle(IntPtr hwnd, Int32 style)
        {
            SetWindowLong(hwnd, WindowLongIndices.GWL_STYLE, style);
        }

        public static void EnableMouseInPointer()
        {
            User32.EnableMouseInPointer(true);
        }

        public static void RegisterWindowClass(String className, WindowClassStyles styles)
        {
            User32.WNDCLASSEX windowClass = new User32.WNDCLASSEX();

            windowClass.cbSize = (UInt32)Marshal.SizeOf(typeof(User32.WNDCLASSEX));
            windowClass.style = (UInt32)styles;
            windowClass.lpfnWndProc = ((hwnd, uMsg, wParam, lParam) =>
            {
                return User32.DefWindowProc(hwnd, uMsg, wParam, lParam);
            });
            windowClass.hInstance = Marshal.GetHINSTANCE(typeof(Win32Interop).Module);
            windowClass.lpszClassName = className;

            User32.RegisterClassEx(ref windowClass);
        }

        public static IntPtr CreateWindow(String className, Int32 styles, int x, int y, int width, int height, IntPtr parentWindow)
        {
            return User32.CreateWindowEx(
                0,
                className,
                String.Empty,
                (UInt32) styles,
                x,
                y,
                width,
                height,
                parentWindow,
                IntPtr.Zero,
                Marshal.GetHINSTANCE(typeof(User32).Module),
                IntPtr.Zero);
        }

        public static void DestroyWindow(IntPtr window)
        {
            User32.DestroyWindow(window);
        }

        public static void FocusWindow(IntPtr window)
        {
            User32.SetFocus(window);
        }

        public static void SetParent(IntPtr window, IntPtr parent)
        {
            IntPtr retVal = User32.SetParent(window, parent);
        }
    }
}
