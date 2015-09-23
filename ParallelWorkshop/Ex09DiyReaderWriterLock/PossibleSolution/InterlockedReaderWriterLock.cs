using System.Threading;

namespace Lurchsoft.ParallelWorkshop.Ex09DiyReaderWriterLock.PossibleSolution
{
    /// <summary>
    /// A scary low-level reader-writer lock implementation.
    /// <para>
    /// This one does not block, though it does yield. It will spin the CPU until the lock is available. However, by doing
    /// so, it avoids the cost of synchronisation and is much faster than a blocking lock.
    /// </para>
    /// </summary>
    public class InterlockedReaderWriterLock : IReaderWriterLock
    {
        private const int OneWriter = 1 << 24;
        private const uint WriterMask = 0xFF000000;

        private int counts;

        public void EnterReadLock()
        {
            while (true)
            {
                int cur = Interlocked.Increment(ref counts);
                if ((cur & WriterMask) == 0)
                {
                    return;
                }

                Interlocked.Decrement(ref counts);
                Thread.Yield();
            }
        }

        public void ExitReadLock()
        {
            Interlocked.Decrement(ref counts);
        }

        public void EnterWriteLock()
        {
            while (true)
            {
                int cur = Interlocked.Add(ref counts, OneWriter);
                if (cur == OneWriter)
                {
                    return;
                }

                Interlocked.Add(ref counts, -OneWriter);
                Thread.Yield();
            }
        }

        public void ExitWriteLock()
        {
            Interlocked.Add(ref counts, -OneWriter);
        }
    }
}
