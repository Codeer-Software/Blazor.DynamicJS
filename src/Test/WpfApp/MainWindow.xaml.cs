using Blazor.DynamicJS;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Threading;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

         //   JSInProcessObjectReference.Sync = Sync;

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddWpfBlazorWebView();
            Resources.Add("services", serviceCollection.BuildServiceProvider());
        }

        object? Sync(Task task)
        {
            while(!task.IsCompleted) DoEvents();

            var type = task.GetType();
            if (type.IsGenericType)
            {
                var helper = (IResultHelper)Activator.CreateInstance(typeof(TaskAwaitHelper<>).MakeGenericType(type.GetGenericArguments().First()),
                    [task])!;
                return helper.Result;
            }
            else
            {
                return null;
            }
        }

        interface IResultHelper
        {
            object? Result { get; }
        }
        class TaskAwaitHelper<T> : IResultHelper
        {
            Task<T> _task;
            public TaskAwaitHelper(Task core) => _task = (Task<T>)core;
            public object? Result => _task.Result;
        }

        public static void DoEvents()
        {
            var frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
        }

        private static object? ExitFrame(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;
            return null;
        }




        ///////
        public static void RunSync(Func<Task> task)
        {
            var oldContext = SynchronizationContext.Current;
            var syncContext = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(syncContext);

            syncContext.Post(async _ =>
            {
                try
                {
                    await task();
                }
                catch (Exception e)
                {
                    syncContext.InnerException = e;
                    throw;
                }
                finally
                {
                    syncContext.EndMessageLoop();
                }
            }, null);

            syncContext.BeginMessageLoop();
            SynchronizationContext.SetSynchronizationContext(oldContext);
            if (syncContext.InnerException != null)
            {
                throw syncContext.InnerException;
            }
        }

        private class ExclusiveSynchronizationContext : SynchronizationContext
        {
            private bool done;
            public Exception InnerException { get; set; }
            private readonly AutoResetEvent workItemsWaiting = new AutoResetEvent(false);
            private readonly Queue<Tuple<SendOrPostCallback, object>> items = new Queue<Tuple<SendOrPostCallback, object>>();

            public override void Send(SendOrPostCallback d, object state)
            {
                throw new NotSupportedException("We cannot send to our same thread");
            }

            public override void Post(SendOrPostCallback d, object state)
            {
                lock (items)
                {
                    items.Enqueue(Tuple.Create(d, state));
                }
                workItemsWaiting.Set();
            }

            public void BeginMessageLoop()
            {
                while (!done)
                {
                    Tuple<SendOrPostCallback, object> task = null;
                    lock (items)
                    {
                        if (items.Count > 0)
                        {
                            task = items.Dequeue();
                        }
                    }
                    if (task != null)
                    {
                        task.Item1(task.Item2);
                        if (InnerException != null) // the method threw an exeption
                        {
                            throw new AggregateException("AsyncHelpers.Run method threw an exception.", InnerException);
                        }
                    }
                    else
                    {
                        workItemsWaiting.WaitOne();
                    }
                }
            }

            public void EndMessageLoop()
            {
                Post(_ => done = true, null);
            }

            public override SynchronizationContext CreateCopy()
            {
                return this;
            }
        }


    }
}