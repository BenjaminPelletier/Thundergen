using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thundergen.UI
{
    class Asynchronizer
    {
        public static Task<T> Wrap<T>(T initialValue, Func<T, T> f)
        {
            var tcs = new TaskCompletionSource<T>();
            var t = new Thread(() =>
            {
                try
                {
                    T result = f(initialValue);
                    tcs.TrySetResult(result);
                }
                catch (TaskCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception e)
                {
                    tcs.TrySetException(e);
                }
            });
            t.Start();
            return tcs.Task;
        }

        public static Task<T> Wrap<T>(Func<T> f)
        {
            var tcs = new TaskCompletionSource<T>();
            var t = new Thread(() =>
            {
                try
                {
                    T result = f();
                    tcs.TrySetResult(result);
                }
                catch (TaskCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception e)
                {
                    tcs.TrySetException(e);
                }
            });
            t.Start();
            return tcs.Task;
        }

        public static Task Wrap(Action f)
        {
            var tcs = new TaskCompletionSource<bool>();
            var t = new Thread(() =>
            {
                try
                {
                    f();
                    tcs.TrySetResult(true);
                }
                catch (TaskCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception e)
                {
                    tcs.TrySetException(e);
                }
            });
            t.Start();
            return tcs.Task;
        }
    }
}
