namespace Lurchsoft.ParallelWorkshop.Ex08DiyReaderWriterLock
{
    public interface IReaderWriterLock
    {
        /// <summary>
        /// Enter a read lock. This will cause any call to <see cref="DiyReaderWriterLock.EnterWriteLock"/> to block, until
        /// <see cref="DiyReaderWriterLock.ExitReadLock"/> is called. However, it will not block other threads attempting
        /// <see cref="DiyReaderWriterLock.EnterReadLock"/>.
        /// </summary>
        void EnterReadLock();

        /// <seealso cref="DiyReaderWriterLock.EnterReadLock"/>
        void ExitReadLock();

        /// <summary>
        /// Enter a write lock. This will cause any call to <see cref="DiyReaderWriterLock.EnterWriteLock"/> or
        /// <see cref="DiyReaderWriterLock.EnterReadLock"/> to block, until <see cref="DiyReaderWriterLock.ExitWriteLock"/> is called.
        /// </summary>
        void EnterWriteLock();

        /// <seealso cref="DiyReaderWriterLock.EnterWriteLock"/>
        void ExitWriteLock();
    }
}