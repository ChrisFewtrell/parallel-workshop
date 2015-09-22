using System.Threading;

namespace Lurchsoft.ParallelWorkshop.Ex08DiyReaderWriterLock
{
    /// <summary>
    /// Try at least the following approaches: 
    /// (1) a reader-writer lock that blocks, releasing the CPU, while waiting to acquire the lock. The
    ///     use of <see cref="Monitor"/> is suggested for this.
    /// (2) a reader-writer lock that spins the CPU, while waiting to acquire the lock. The use of
    ///     <see cref="Interlocked"/> is suggested for this. This is quite a tricky exercise!
    /// </summary>
    public class DiyReaderWriterLock : IReaderWriterLock
    {
        /// <summary>
        /// Enter a read lock. This will cause any call to <see cref="EnterWriteLock"/> to block, until
        /// <see cref="ExitReadLock"/> is called. However, it will not block other threads attempting
        /// <see cref="EnterReadLock"/>.
        /// </summary>
        public void EnterReadLock()
        {
            // TODO: implement this
        }

        /// <seealso cref="EnterReadLock"/>
        public void ExitReadLock()
        {
            // TODO: implement this
        }

        /// <summary>
        /// Enter a write lock. This will cause any call to <see cref="EnterWriteLock"/> or
        /// <see cref="EnterReadLock"/> to block, until <see cref="ExitWriteLock"/> is called.
        /// </summary>
        public void EnterWriteLock()
        {
            // TODO: implement this
        }

        /// <seealso cref="EnterWriteLock"/>
        public void ExitWriteLock()
        {
            // TODO: implement this
        }
    }
}
