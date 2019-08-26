
using System.Windows.Threading;

namespace System.Threading.Tasks
{
    internal static class TaskExtensions
    {
        public static T GetResultWhileDoingEvents<T>(this Task<T> task)
        {
            while (!task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
            {
                Dispatcher.CurrentDispatcher.DoEvents();
            }

            return task.Result;
        }
    }
}
