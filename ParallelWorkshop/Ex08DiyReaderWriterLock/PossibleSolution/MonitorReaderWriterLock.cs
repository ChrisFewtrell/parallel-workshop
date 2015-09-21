using System.Threading;

namespace Lurchsoft.ParallelWorkshop.Ex08DiyReaderWriterLock.PossibleSolution
{
    /// <summary>
    /// A rather unsophisticated (for which read "not very fast") reader-writer lock implementation. Hopefully, it
    /// is at least correct.
    /// <para>
    /// Can you do better?
    /// </para>
    /// </summary>
    public class MonitorReaderWriterLock : IReaderWriterLock
    {
        private readonly object sync = new object();
        private int readerCount;
        private bool writing;

        public void EnterReadLock()
        {
            lock (sync)
            {
                while (writing)
                {
                    Monitor.Wait(sync);
                }

                ++readerCount;
            }
        }

        public void ExitReadLock()
        {
            lock (sync)
            {
                --readerCount;
                Monitor.Pulse(sync);
            }
        }

        public void EnterWriteLock()
        {
            Monitor.Enter(sync);
            
            while (writing || readerCount > 0)
            {
                Monitor.Wait(sync);
            }

            writing = true;
        }

        public void ExitWriteLock()
        {
            writing = false;
            Monitor.Pulse(sync);
            Monitor.Exit(sync);
        }
    }
}
