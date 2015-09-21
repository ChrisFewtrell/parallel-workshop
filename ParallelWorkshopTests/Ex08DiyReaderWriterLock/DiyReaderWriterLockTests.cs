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
            var state = new State(new DiyReaderWriterLock());
            var tasks = StartTasks(4, 50000, state, PerformReads).Concat(StartTasks(2, 10000, state, PerformWrites)).ToArray();

            Task.WaitAll(tasks); // will throw AggregateException if any task throws an exception
        }

        [Test]
        public void Lock_ShouldAllowMultipleSimultaneousReaders()
        {
            var state = new State(new DiyReaderWriterLock());
            var tasks = StartTasks(4, 5000, state, PerformReads).Concat(StartTasks(1, 1000, state, PerformWrites)).ToArray();
            
            Task.WaitAll(tasks);
            Assert.That(state.MaxSimultaneousReaders, Is.GreaterThan(1));
        }

        private IEnumerable<Task> StartTasks(int numTasks, int taskSize, State state, Action<int, State> action)
        {
            return Enumerable.Range(0, numTasks).Select(i => Task.Factory.StartNew(() => action(taskSize, state)));
        }

        private static void PerformWrites(int numWrites, State state)
        {
            for (int i = 0; i < numWrites; ++i)
            {
                state.Add(GetUniqueString(), GetUniqueString());
            }
        }

        private static void PerformReads(int numReads, State state)
        {
            for (int i = 0; i < numReads; ++i)
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                state.AllValues.ToList();
            }
        }

        private static string GetUniqueString()
        {
            return Guid.NewGuid().ToString("D");
        }

        private class State
        {
            private readonly Dictionary<string, string> values = new Dictionary<string, string>();
            private readonly IReaderWriterLock readerWriterLock;
            private int curSimultaneousReaders, maxSimultaneousReaders;

            public State(IReaderWriterLock readerWriterLock)
            {
                this.readerWriterLock = readerWriterLock;
            }

            public int MaxSimultaneousReaders
            {
                get { return maxSimultaneousReaders; }
            }

            public IEnumerable<string> AllValues
            {
                get
                {
                    int readers = Interlocked.Increment(ref curSimultaneousReaders);
                    Interlocked.CompareExchange(ref maxSimultaneousReaders, readers, readers - 1);
                    readerWriterLock.EnterReadLock();
                    try
                    {
                        return values.Values.ToList();
                    }
                    finally
                    {
                        readerWriterLock.ExitReadLock();
                        Interlocked.Decrement(ref curSimultaneousReaders);
                    }
                }
            }

            public void Add(string key, string value)
            {
                readerWriterLock.EnterWriteLock();
                try
                {
                    values[key] = value;
                }
                finally
                {
                    readerWriterLock.ExitWriteLock();
                }
            }
        }
    }
}
