using System.Collections.Generic;

namespace System.Windows.Threading
{
    public static class DispatcherExtensions
    {
        private static Dictionary<Dispatcher, int> disabledDispatchers = new Dictionary<Dispatcher, int>();

        private struct PumpDisabler : IDisposable
        {
            DispatcherProcessingDisabled disabled;
            Dispatcher dispatcher;

            public PumpDisabler(Dispatcher dispatcher)
            {
                this.dispatcher = dispatcher;

                this.disabled = this.dispatcher.DisableProcessing();

                if (dispatcher.IsDisabled())
                {
                    disabledDispatchers[this.dispatcher]++;
                }
                else
                {
                    disabledDispatchers.Add(dispatcher, 1);
                }
            }

            public void Dispose()
            {
                this.disabled.Dispose();

                var refCount = --disabledDispatchers[this.dispatcher];
                if (refCount == 0)
                {
                    disabledDispatchers.Remove(this.dispatcher);
                }
            }
        }

        public static IDisposable DisablePumping(this Dispatcher dispatcher)
        {
            return new PumpDisabler(dispatcher);
        }

        public static bool IsDisabled(this Dispatcher dispatcher)
        {
            return disabledDispatchers.ContainsKey(dispatcher);
        }

        /// <summary>
        /// Equivalent to DoEvents
        /// </summary>
        /// <seealso cref="http://msdn.microsoft.com/en-us/library/system.windows.threading.dispatcher.pushframe.aspx"/>
        public static void DoEvents(this Dispatcher dispatcher)
        {
            if (dispatcher.IsDisabled())
            {
                return;
            }

            var frame = new DispatcherFrame();
            dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action<DispatcherFrame>((p) =>
            {
                p.Continue = false;
            }), frame);

            Dispatcher.PushFrame(frame);
        }
    }
}
