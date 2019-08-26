using System;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace MediaTestManaged
{
    public static class VisualTreeUtils
    {
        public static FrameworkElement FindNameInSubtree(this FrameworkElement element, string descendantName)
        {
            if (element == null)
                return null;

            if (element.Name == descendantName)
                return element;

            int childrenCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childrenCount; i++)
            {
                var result = FindNameInSubtree(VisualTreeHelper.GetChild(element, i) as FrameworkElement, descendantName);
                if (result != null)
                    return result;
            }

            return null;
        }

        public static T FindElementOfTypeInSubtree<T>(this DependencyObject element)
            where T : DependencyObject
        {
            if (element == null)
                return null;

            if (element is T)
                return (T)element;

            int childrenCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childrenCount; i++)
            {
                var result = FindElementOfTypeInSubtree<T>(VisualTreeHelper.GetChild(element, i));
                if (result != null)
                    return result;
            }

            return null;
        }
    }

    public abstract class EventTester<TSender, TEventArgs> : IDisposable
    {
        private readonly TSender sender;
        private object lastSender;
        private int executeCount;

        public EventTester(TSender sender)
        {
            this.lastSender = null;
            this.executeCount = 0;
            this.sender = sender;
            this.AddEvent();
        }

        protected TSender Sender
        {
            get
            {
                return this.sender;
            }
        }

        protected void OnEventFired(object sender, TEventArgs e)
        {
            this.lastSender = sender;
            this.executeCount++;
        }

        protected abstract void AddEvent();

        protected abstract void RemoveEvent();

        public int ExecuteCount
        {
            get
            {
                return executeCount;
            }
        }

        public object LastSender
        {
            get
            {
                return lastSender;
            }
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    this.RemoveEvent();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }

    public sealed class DoubleTappedEventTester: EventTester<UIElement, DoubleTappedRoutedEventArgs>
    {
        public DoubleTappedEventTester(UIElement sender)
            : base(sender)
        {
        }

        protected override void AddEvent()
        {
            this.Sender.DoubleTapped += this.OnEventFired;
        }

        protected override void RemoveEvent()
        {
            this.Sender.DoubleTapped -= this.OnEventFired;
        }
    }

    public sealed class MediaEndedEventTester : EventTester<MediaPlayer, object>
    {
        public MediaEndedEventTester(MediaPlayer sender)
            : base(sender)
        {
        }

        protected override void AddEvent()
        {
            this.Sender.MediaEnded += this.OnEventFired;
        }

        protected override void RemoveEvent()
        {
            this.Sender.MediaEnded -= this.OnEventFired;
        }
    }

}
