using System;
using System.Threading;
using UnderstandingThreadPool.Models;

namespace UnderstandingThreadPool
{
    // There is a cost with starting a new thread, so for purposes of efficiency, the thread pool
    // holds onto created(but inactive) threads until needed. To allow you to interact with this pool of waiting threads,
    // the System.Threading namespace provides the ThreadPool class type. If you want to queue a method call for processing
    // by a worker thread in the pool, you can use the ThreadPool.QueueUserWorkItem() method.
    // This method has been overloaded to allow you to specify an optional System.Object for custom state data
    // in addition to an instance of the WaitCallback delegate.
    //public static class ThreadPool
    //{
    //    ...
    //    public static bool QueueUserWorkItem(WaitCallback callBack);
    //    public static bool QueueUserWorkItem(WaitCallback callBack, object state);
    //}

    // The benefits of leveraging the thread pool:
    // • The thread pool manages threads efficiently by minimizing the number of threads
    //   that must be created, started, and stopped.
    // • By using the thread pool, you can focus on your business problem rather than the
    //   application’s threading infrastructure.
    // However, using manual thread management is preferred in some cases. Here is an example:
    // • If you require foreground threads or must set the thread priority.Pooled threads are
    //   always background threads with default priority(ThreadPriority.Normal).
    // • If you require a thread with a fixed identity to abort it, suspend it, or discover it by name.
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Main thread started. ThreadID = {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("ThreadPool Count: {0}", ThreadPool.ThreadCount);
            Console.WriteLine();
            Printer p = new Printer();
            WaitCallback workItem = new WaitCallback(PrintTheNumbers);

            // Queue the method ten times.
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(workItem, p);
            }

            Console.WriteLine("All tasks queued");
            Console.ReadLine();
        }

        static void PrintTheNumbers(object state)
        {
            Printer task = (Printer)state;
            task.PrintNumbers();
        }
    }
}