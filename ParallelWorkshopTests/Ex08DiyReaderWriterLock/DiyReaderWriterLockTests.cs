using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lurchsoft.ParallelWorkshop.Ex08DiyReaderWriterLock;
using NUnit.Framework;

namespace Lurchsoft.ParallelWorkshopTests.Ex08DiyReaderWriterLock
{
    public class DiyReaderWriterLockTests
    {
        [Test]
        public void Lock_ShouldProtectThreadUnsafeCollectionAgainstUnsafeModification()
        {
            var list = new ReaderWriterLockedList(new DiyReaderWriterLock());
            Task[] tasks = PrepareTasks(4, 20000, list, PerformReads).Concat(PrepareTasks(2, 6000, list, PerformWrites)).ToArray();

            Task.WaitAll(tasks); // will throw AggregateException if any task throws an exception
        }

        [Test]
        public void Lock_ShouldAllowMultipleSimultaneousReaders()
        {
            var list = new ReaderWriterLockedList(new DiyReaderWriterLock());
            Task[] tasks = PrepareTasks(4, 5000, list, PerformReads).Concat(PrepareTasks(1, 1000, list, PerformWrites)).ToArray();
            
            Task.WaitAll(tasks);
            Assert.That(list.MaxSimultaneousReaders, Is.GreaterThan(1));
        }

        private IEnumerable<Task> PrepareTasks(int numTasks, int taskSize, ReaderWriterLockedList state, Action<int, ReaderWriterLockedList> action)
        {
            return Enumerable.Range(0, numTasks).Select(i => Task.Factory.StartNew(() => action(taskSize, state)));
        }

        private static void PerformWrites(int numWrites, ReaderWriterLockedList state)
        {
            for (int i = 0; i < numWrites; ++i)
            {
                state.Add(Guid.NewGuid().ToString("D"));
            }
        }

        private static void PerformReads(int numReads, ReaderWriterLockedList state)
        {
            for (int i = 0; i < numReads; ++i)
            {
                state.GetAllValues();
            }
        }

        /// <summary>This wraps a <see cref="List{T}"/>, exposing one read and one write operation, protected by a reader-writer lock.</summary>
        private class ReaderWriterLockedList
        {
            private readonly List<string> values = new List<string>();

            private readonly IReaderWriterLock readerWriterLock;
            private int curSimultaneousReaders, maxSimultaneousReaders;

            public ReaderWriterLockedList(IReaderWriterLock readerWriterLock)
            {
                this.readerWriterLock = readerWriterLock;
            }

            public int MaxSimultaneousReaders
            {
                get { return maxSimultaneousReaders; }
            }

            public IEnumerable<string> GetAllValues()
            {
                int readers = Interlocked.Increment(ref curSimultaneousReaders);
                Interlocked.CompareExchange(ref maxSimultaneousReaders, readers, readers - 1);
                readerWriterLock.EnterReadLock();
                try
                {
                    return new List<string>(values); // snapshot
                }
                finally
                {
                    readerWriterLock.ExitReadLock();
                    Interlocked.Decrement(ref curSimultaneousReaders);
                }
            }

            public void Add(string value)
            {
                readerWriterLock.EnterWriteLock();
                try
                {
                    values.Add(value);
                }
                finally
                {
                    readerWriterLock.ExitWriteLock();
                }
            }
        }
    }
}
