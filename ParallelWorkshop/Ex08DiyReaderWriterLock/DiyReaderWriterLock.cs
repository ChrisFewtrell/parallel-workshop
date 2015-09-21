using System.Threading;

namespace Lurchsoft.ParallelWorkshop.Ex08DiyReaderWriterLock
{
    /// <summary>
    /// A lock that allows multiple simultaneous reads but only one simultaneous write, which also
    /// can only occur once all reads have completed.
    /// <para>
    /// Purely for fun and education, this should be a Do-It-Yourself implementation of reader-writer
    /// lock semantics. You can use <c>lock()</c>, <see cref="Interlocked"/> and <see cref="Monitor"/> but obviously
    /// you are not allowed to use <see cref="ReaderWriterLock"/> or any similar Framework classes.
    /// </para>
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
