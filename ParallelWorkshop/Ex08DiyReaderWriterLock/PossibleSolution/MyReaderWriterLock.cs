using System.Threading;

namespace Lurchsoft.ParallelWorkshop.Ex08DiyReaderWriterLock.PossibleSolution
{
    public class MyReaderWriterLock : IReaderWriterLock
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
                Monitor.PulseAll(sync);
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
            Monitor.PulseAll(sync);
            Monitor.Exit(sync);
        }
    }
}
