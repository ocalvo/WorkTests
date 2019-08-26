using Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows;
using System.Diagnostics;
using System.Windows.Input;
using Windows.UI.Core;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Automation.Peers;
using System.Reflection;
using System.IO;

namespace Interop
{
    public class JupiterHost : HwndHost
    {
        public static DependencyProperty ControlTypeProperty = DependencyProperty.Register("ControlType", typeof(String), typeof(JupiterHost));

        private const String WINDOW_CLASS_NAME = "JupiterHostWindowClass";

        public JupiterHost()
        {
            // uncomment these event handlers to debug focus issues
            //this.GotFocus += JupiterHost_GotFocus;
            //this.GotKeyboardFocus += JupiterHost_GotKeyboardFocus;
            //this.LostFocus += JupiterHost_LostFocus;
            //this.LostKeyboardFocus += JupiterHost_LostKeyboardFocus;
        }

        public String ControlType
        {
            get
            {
                return (String)GetValue(ControlTypeProperty);
            }
            set
            {
                SetValue(ControlTypeProperty, value);
            }
        }

        private void JupiterHost_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Debug.WriteLine(String.Format("JupiterHost_LostKeyboardFocus"));
        }

        private void JupiterHost_LostFocus(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(String.Format("JupiterHost_LostFocus"));
        }

        private void JupiterHost_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Debug.WriteLine(String.Format("JupiterHost_GotKeyboardFocus"));
        }

        private void JupiterHost_GotFocus(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(String.Format("JupiterHost_GotFocus"));
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size desiredSize = new Size(0, 0);

            Windows.UI.Xaml.UIElement rootXamlElement = Windows.UI.Xaml.Window.Current.Content;

            if (rootXamlElement != null)
            {
                rootXamlElement.Measure(new Windows.Foundation.Size(availableSize.Width, availableSize.Height));
                desiredSize.Width = rootXamlElement.DesiredSize.Width;
                desiredSize.Height = rootXamlElement.DesiredSize.Height;
            }

            desiredSize.Width = Math.Min(desiredSize.Width, availableSize.Width);
            desiredSize.Height = Math.Min(desiredSize.Height, desiredSize.Height);

            // uncomment to debug layout issues
            //Debug.WriteLine("JupiterHost Measure({0}x{1}), DesiredSize= {2}x{3}", availableSize.Width, availableSize.Height, desiredSize.Width, desiredSize.Height);

            return desiredSize;
        }

        private IntPtr InitializePresenterHwndMode(HandleRef hwndParent)
        {
            Win32Interop.RegisterWindowClass(WINDOW_CLASS_NAME, WindowClassStyles.CS_HREDRAW | WindowClassStyles.CS_VREDRAW);

            IntPtr windowHandle = Win32Interop.CreateWindow(
                WINDOW_CLASS_NAME,
                WindowStyles.WS_CHILD | WindowStyles.WS_VISIBLE | WindowStyles.WS_CLIPCHILDREN,
                0,
                0,
                0,
                0,
                hwndParent.Handle);

            XamlPresenterStatics xamlPresenterStatics = new XamlPresenterStatics();
            XamlPresenter presenter = xamlPresenterStatics.CreateFromHwnd((int)windowHandle);

            presenter.InitializePresenter();

            return windowHandle;
        }

        private IntPtr InitializePresenterCoreWindowMode(HandleRef hwndParent)
        {
            CoreWindow coreWindow = CoreWindowInterop.CreateCoreWindow("XAMLONWIN32", 0, 0, 0, 0);

            ICoreApplicationPrivate2 coreApp = new ICoreApplicationPrivate2();
            coreApp.CreateNonImmersiveView();

            XamlPresenterStatics3 xamlPresenterStatics = new XamlPresenterStatics3();
            XamlPresenter presenter = xamlPresenterStatics.CreateFromCoreWindow(coreWindow);

            ICoreWindowInterop interop = new ICoreWindowInterop(coreWindow);
            IntPtr windowHandle = interop.GetWindowHandle();

            int style = Win32Interop.GetWindowStyle(windowHandle);
            style = style & (~WindowStyles.WS_POPUP);
            style = style | WindowStyles.WS_CHILD;
            Win32Interop.SetWindowStyle(windowHandle, style);
            Win32Interop.SetParent(windowHandle, hwndParent.Handle);

            presenter.InitializePresenter();

            return windowHandle;
        }

        private IntPtr InitializeBridge(HandleRef hwndParent)
        {
            int hostHwnd;

            XamlBridgeStatics xamlBridgeStatics = new XamlBridgeStatics();
            XamlBridge bridge = xamlBridgeStatics.CreateForParentHwnd((int)hwndParent.Handle, out hostHwnd);

            bridge.InitializeBridge();

            return new IntPtr(hostHwnd);
        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            Win32Interop.EnableMouseInPointer();

            // There are several different low-level interfaces available at this time.
            // Use XamlBridge unless you have a known reason to use the others.
            //IntPtr windowHandle = InitializePresenterHwndMode(hwndParent);
            //IntPtr windowHandle = InitializePresenterCoreWindowMode(hwndParent);
            IntPtr windowHandle = InitializeBridge(hwndParent);

            // Uncomment the following lines to create Xaml UI content directly from within this WPF host control.
            // Normally, the user of this control sets a property which is the name of a Xaml UserControl to instantiate.

            Windows.UI.Xaml.Controls.StackPanel panel = new Windows.UI.Xaml.Controls.StackPanel();
            panel.Orientation = Windows.UI.Xaml.Controls.Orientation.Vertical;
            panel.Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);

            Windows.UI.Xaml.Controls.TextBox textBox = new Windows.UI.Xaml.Controls.TextBox();
            textBox.Text = "XAML TextBox 1";
            panel.Children.Add(textBox);

            Windows.UI.Xaml.Controls.Button button = new Windows.UI.Xaml.Controls.Button();
            button.Content = "XAML Button";
            panel.Children.Add(button);

            textBox = new Windows.UI.Xaml.Controls.TextBox();
            textBox.Text = "XAML TextBox 2";
            panel.Children.Add(textBox);

            //XamlClassLibrary.TestUserControl testUserControl = new XamlClassLibrary.TestUserControl();
            //panel.Children.Add(testUserControl);

            Windows.UI.Xaml.Window.Current.Content = panel;

            panel.LayoutUpdated += XamlContentLayoutUpdated;
            

            XamlApplication application = new XamlApplication();
            String globalResourceFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "globalresources.xbf");
            if (File.Exists(globalResourceFile))
            {
                Windows.UI.Xaml.Application.LoadComponent(application, new Uri("ms-resource:///Files/globalresources.xaml"));
            }

            Windows.UI.Xaml.FrameworkElement contentRoot = panel;
            //var type = Type.GetType(ControlType, true, true);
            //Windows.UI.Xaml.FrameworkElement contentRoot = (Windows.UI.Xaml.FrameworkElement) Activator.CreateInstance("XamlClassLibrary", ControlType).Unwrap();

            Windows.UI.Xaml.Window.Current.Content = contentRoot;

            contentRoot.LayoutUpdated += XamlContentLayoutUpdated;

            return new HandleRef(this, windowHandle);
        }

        private void XamlContentLayoutUpdated(object sender, object e)
        {
            // uncomment these lines to debug layout
            // Windows.UI.Xaml.UIElement rootXamlElement = Windows.UI.Xaml.Window.Current.Content;
            //Debug.WriteLine(String.Format("Layout updated: {0} {1}", rootXamlElement.RenderSize.Width, rootXamlElement.RenderSize.Height));

            InvalidateMeasure();
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            Win32Interop.DestroyWindow(hwnd.Handle);
            
            // TODO: this is the best place to explicitly tear down the XamlBridge
        }

        protected override bool TabIntoCore(TraversalRequest request)
        {
            Windows.UI.Xaml.DependencyObject elementToFocus;

            if (request.FocusNavigationDirection == FocusNavigationDirection.First)
            {
                elementToFocus = Windows.UI.Xaml.Input.FocusManager.FindFirstFocusableElement(null);
            }
            else
            {
                elementToFocus = Windows.UI.Xaml.Input.FocusManager.FindLastFocusableElement(null);
            }
            
            if (elementToFocus is Windows.UI.Xaml.Controls.Control)
            {
                Win32Interop.FocusWindow(Handle);
                ((Windows.UI.Xaml.Controls.Control)elementToFocus).Focus(Windows.UI.Xaml.FocusState.Keyboard);
                return true;
            }

            return false;
        }

        protected override bool TranslateAcceleratorCore(ref MSG msg, ModifierKeys modifiers)
        {
            if (msg.message == WindowMessages.WM_KEYDOWN && msg.wParam.ToInt32() == VirtualKeyCodes.VK_TAB)
            {
                object currentlyFocusedElement = Windows.UI.Xaml.Input.FocusManager.GetFocusedElement();

                if (ModifierKeys.Shift == modifiers)
                {
                    Windows.UI.Xaml.DependencyObject firstFocusableElement = Windows.UI.Xaml.Input.FocusManager.FindFirstFocusableElement(null);

                    if (currentlyFocusedElement == firstFocusableElement)
                    {
                        bool focusDeparted = ((IKeyboardInputSink)this).KeyboardInputSite.OnNoMoreTabStops(new TraversalRequest(FocusNavigationDirection.Previous));
                        if (focusDeparted)
                        {
                            // need xaml to lose focus
                            Windows.UI.Xaml.Controls.Control currentlyFocusedControl = (Windows.UI.Xaml.Controls.Control)currentlyFocusedElement;
                            currentlyFocusedControl.IsTabStop = false;
                            currentlyFocusedControl.IsEnabled = false;
                            currentlyFocusedControl.IsTabStop = true;
                            currentlyFocusedControl.IsEnabled = true;
                        }
                        return focusDeparted;
                    }

                    return Windows.UI.Xaml.Input.FocusManager.TryMoveFocus(Windows.UI.Xaml.Input.FocusNavigationDirection.Previous);
                }
                else
                {
                    Windows.UI.Xaml.DependencyObject lastFocusableElement = Windows.UI.Xaml.Input.FocusManager.FindLastFocusableElement(null);

                    if (currentlyFocusedElement == lastFocusableElement)
                    {
                        bool focusDeparted = ((IKeyboardInputSink)this).KeyboardInputSite.OnNoMoreTabStops(new TraversalRequest(FocusNavigationDirection.Next));
                        if (focusDeparted)
                        {
                            // need xaml to lose focus
                            Windows.UI.Xaml.Controls.Control currentlyFocusedControl = (Windows.UI.Xaml.Controls.Control)currentlyFocusedElement;
                            currentlyFocusedControl.IsTabStop = false;
                            currentlyFocusedControl.IsEnabled = false;
                            currentlyFocusedControl.IsTabStop = true;
                            currentlyFocusedControl.IsEnabled = true;
                        }
                        return focusDeparted;
                    }

                    return Windows.UI.Xaml.Input.FocusManager.TryMoveFocus(Windows.UI.Xaml.Input.FocusNavigationDirection.Next);
                }
            }

            return false;
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (WindowMessages.WM_POINTERDOWN == msg)
            {
                // This is needed when using hwnd interop. Not needed when using CoreWindow interop. Keep commented unless specifically debugging an hwnd based scenario without a CoreWindow.
                //Win32Interop.FocusWindow(Handle);                
            }

            return base.WndProc(hwnd, msg, wParam, lParam, ref handled);
        }        
    }
}
