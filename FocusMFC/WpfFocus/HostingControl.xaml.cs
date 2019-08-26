using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.UI.Core;
using Windows.UI.Core.Input;

namespace Windows.UI.Core.Input
{
    public enum CoreNavigationReason
    {
        //  Programmatic focus setting
        Programmatic = 0,
        // Focus restoration for task switching
        Restore = 1,
        // AccessKey NavigationClient
        AccessKey = 2,
        // Semantic (non-Cartesian), bidirectional navigation (e.g. Tab/Shift-Tab,
        // F6/Shift-F6)
        Next = 3,
        Previous = 4,
        //NextGroup = 5,
        //PreviousGroup = 6,
        // Cartesian four-directional navigation (e.g. D-pad, keyboard arrow keys)
        Left = 7,
        Up = 8,
        Right = 9,
        Down = 10,
    }

    public class NavigationFocusEventArgs
    {
        public CoreNavigationReason Reason { get; }
        public Rect Origin { get; }
        public Guid CorrelationId { get; }
        public bool Handled { get; set; }
    }

    public class NavigateFocusResult
    {
        public bool FocusMoved { get; }
    }

    public interface IUIBridgeFocus
    {
        Task<NavigateFocusResult> NavigateFocusAsync(CoreNavigationReason reason, Rect origin, Guid correlationId);
        event EventHandler<NavigationFocusEventArgs> FocusDeparting;
    }
}

namespace Windows.UI.Xaml.Hosting
{
    public class XamlBridge : IUIBridgeFocus
    {
        public event EventHandler<NavigationFocusEventArgs> FocusDeparting
        {
            add
            {

            }
            remove
            {

            }
        }

        public async Task<NavigateFocusResult> NavigateFocusAsync(NavigationReason reason, Rect origin, Guid guid)
        {
            await Task.Delay(10);
            return new NavigateFocusResult();
        }
    }
}
public sealed class XamlHost : System.Windows.Interop.HwndHost
{
    private readonly Windows.UI.Core.Input.IUIBridgeFocus xamlBridge;

    public XamlHost()
    {
        this.xamlBridge = new Windows.UI.Xaml.Hosting.XamlBridge();
        this.xamlBridge.FocusDeparting += OnFocusDeparting;
    }

    private readonly static Dictionary<CoreNavigationReason, FocusNavigationDirection>
      mapReasonToDirection = new Dictionary<CoreNavigationReason, FocusNavigationDirection>{
            { CoreNavigationReason.Next, FocusNavigationDirection.Next },
            { CoreNavigationReason.Previous, FocusNavigationDirection.Next },
            { CoreNavigationReason.Up, FocusNavigationDirection.Up },
            { CoreNavigationReason.Down, FocusNavigationDirection.Down },
            { CoreNavigationReason.Left, FocusNavigationDirection.Left },
            { CoreNavigationReason.Right, FocusNavigationDirection.Right },
    };

    private Dictionary<Guid, UIElement> elementsMap;

    private void OnFocusDeparting(object sender, NavigationFocusEventArgs e)
    {
        if (e.CorrelationId != Guid.Empty && e.Reason == CoreNavigationReason.Programmatic)
        {
            UIElement element = elementsMap[e.CorrelationId];
            e.Handled = element.Focus();
        }

        if (!e.Handled)
        {
            var direction = mapReasonToDirection[e.Reason];
            var request = new TraversalRequest(direction);
            e.Handled = this.MoveFocus(request);
        }
    }

    private readonly static Dictionary<FocusNavigationDirection, CoreNavigationReason> mapDirectionToReason =
        new Dictionary<FocusNavigationDirection, CoreNavigationReason>
        {
            { FocusNavigationDirection.Next, CoreNavigationReason.Next },
            { FocusNavigationDirection.First , CoreNavigationReason.Next },
            { FocusNavigationDirection.Previous, CoreNavigationReason.Previous },
            { FocusNavigationDirection.Last , CoreNavigationReason.Previous },
            { FocusNavigationDirection.Up, CoreNavigationReason.Up },
            { FocusNavigationDirection.Down , CoreNavigationReason.Down },
            { FocusNavigationDirection.Left, CoreNavigationReason.Left },
            { FocusNavigationDirection.Right , CoreNavigationReason.Right },
        };


    protected override bool TabIntoCore(TraversalRequest request)
    {
        var origin = new Rect();
        //if (this.PreviousFocusedElement != null)
        {
            //var focusedElement = this.PreviousFocusedElement;
            //var p1 = focusedElement.PointFromScreen(new Point(0, 0));
            //var p2 = focusedElement.PointFromScreen(new Point(focusedElement.ActualHeight, focusedElement.ActualWidth));
            //origin = new Rect(p1, p2);
        }

        var reason = mapDirectionToReason[request.FocusNavigationDirection];
        var task = this.xamlBridge.NavigateFocusAsync(reason, origin, Guid.Empty);
        var result = task.GetResultWhileDoingEvents();
        return result.FocusMoved;
    }

    protected override HandleRef BuildWindowCore(HandleRef hwndParent)
    {
        throw new NotImplementedException();
    }

    protected override async void OnAccessKey(AccessKeyEventArgs e)
    {
        base.OnAccessKey(e);

        await this.xamlBridge.NavigateFocusAsync(CoreNavigationReason.AccessKey, new Rect(), Guid.Empty);
    }

    protected override void DestroyWindowCore(HandleRef hwnd)
    {
        throw new NotImplementedException();
    }
}

namespace WpfFocus
{
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Interop;


/// <summary>
/// Interaction logic for HostingControl.xaml
/// </summary>
    public partial class HostingControl : Button
    {
        private Windows.UI.Xaml.Hosting.XamlBridge xamlBridge = new Windows.UI.Xaml.Hosting.XamlBridge();

        public HostingControl()
        {
           // HwndHost h = new HwndHost();
            InitializeComponent();

            this.xamlBridge.FocusDeparting += OnFocusDeparting;
        }

        private readonly static Dictionary<NavigationReason, FocusNavigationDirection>
          mapReasonToDirection = new Dictionary<NavigationReason, FocusNavigationDirection>{
            { NavigationReason.Next, FocusNavigationDirection.Next },
            { NavigationReason.Previous, FocusNavigationDirection.Next },
            { NavigationReason.Up, FocusNavigationDirection.Up },
            { NavigationReason.Down, FocusNavigationDirection.Down },
            { NavigationReason.Left, FocusNavigationDirection.Left },
            { NavigationReason.Right, FocusNavigationDirection.Right },
        };

        private void OnFocusDeparting(object sender, NavigationFocusEventArgs e)
        {
            var direction = mapReasonToDirection[e.Reason];
            var request = new TraversalRequest(direction);
            this.MoveFocus(request);
        }

        public UIElement PreviousElement { get; set; }
        public UIElement NextElement { get; set; }

        private readonly static Dictionary<Key, NavigationReason> keyMap = new Dictionary<Key, NavigationReason> {
                { Key.Tab, NavigationReason.Next },
                { Key.Up, NavigationReason.Up },
                { Key.Down, NavigationReason.Down },
                { Key.Left, NavigationReason.Left },
                { Key.Right, NavigationReason.Right },
            };

        private async Task<CoreNavigationReason> GetReasonFromKeyboardState()
        {
            var keyboardDevice = InputManager.Current.PrimaryKeyboardDevice;
            var shiftKeyState = keyboardDevice.GetKeyStates(Key.LeftShift) | keyboardDevice.GetKeyStates(Key.RightShift);
            var kvp = keyMap.FirstOrDefault(kv => (keyboardDevice.GetKeyStates(kv.Key) & KeyStates.Down)== KeyStates.Down);

            await Task.Delay(1000);

            if (kvp.Value == CoreNavigationReason.Next)
            {
                return ((shiftKeyState & KeyStates.Down) == KeyStates.Down) ? NavigationReason.Previous : NavigationReason.Next;
            }
            else
            {
                return kvp.Value;
            }

        }

        private async Task<int> Foo()
        {
            await Task.Delay(100000);
            return 0;
        }

        protected override async void OnPreviewGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            var task = GetReasonFromKeyboardState();
            var reason = task.GetResultWhileDoingEvents();

            var origin = new Rect();
            var focusedElement = e.OldFocus as FrameworkElement;
            if (focusedElement != null)
            {
                var p1 = focusedElement.PointFromScreen(new Point(0, 0));
                var p2 = focusedElement.PointFromScreen(new Point(focusedElement.ActualWidth, focusedElement.ActualHeight));
                origin = new Rect(p1, p2);
            }

            base.OnPreviewGotKeyboardFocus(e);

            //this.Foo().Wait();
            this.Foo().GetResultWhileDoingEvents();

            var result = await this.xamlBridge.NavigateFocusAsync(reason, origin);
            if (!result.FocusMoved)
            {
                //var direction = mapReasonToDirection[reason];
                //var request = new TraversalRequest(direction);
                //this.MoveFocus(request);
            }
        }

        protected override async void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            await this.xamlBridge.NavigateFocusAsync(CoreNavigationReason.Restore, new Rect(), Guid.Empty);
        }

        /*
        protected override async void OnAccessKey(AccessKeyEventArgs e)
        {
            base.OnAccessKey(e);

            var reason = NavigationReason.AccessKey;
            var args = this.xamlBridge.CreateNavigationFocusEventArgs(reason, new Rect());
            await this.xamlBridge.NavigateFocusAsync(args);
        }*/
    }
}
