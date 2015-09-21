using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lurchsoft.ParallelWorkshop.Ex08DiyReaderWriterLock;
using Lurchsoft.ParallelWorkshop.Ex08DiyReaderWriterLock.PossibleSolution;
using NUnit.Framework;

namespace Lurchsoft.ParallelWorkshopTests.Ex08DiyReaderWriterLock
{
    public class DiyReaderWriterLockTests
    {
        [Test]
        public void Lock_ShouldProtectThreadUnsafeCollectionAgainstUnsafeModification()
        {
            const int NumReads = 10000, NumWrites = 30000;
            var state = new State(new MyReaderWriterLock());

            var reader1 = Task.Factory.StartNew(() => PerformReads(NumReads, state));
            var reader2 = Task.Factory.StartNew(() => PerformReads(NumReads, state));
            var reader3 = Task.Factory.StartNew(() => PerformReads(NumReads, state));
            var reader4 = Task.Factory.StartNew(() => PerformReads(NumReads, state));
            var writer1 = Task.Factory.StartNew(() => PerformWrites(NumWrites, state));
            var writer2 = Task.Factory.StartNew(() => PerformWrites(NumWrites, state));

            Task.WaitAll(reader1, reader2, reader3, reader4, writer1, writer2);
        }

        private void PerformWrites(int numWrites, State state)
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

        private string GetUniqueString()
        {
            return Guid.NewGuid().ToString("D");
        }

        private class State
        {
            private readonly Dictionary<string, string> values = new Dictionary<string, string>();
            private readonly IReaderWriterLock readerWriterLock;

            public State(IReaderWriterLock readerWriterLock)
            {
                this.readerWriterLock = readerWriterLock;
            }

            public IEnumerable<string> AllValues
            {
                get
                {
                    readerWriterLock.EnterReadLock();
                    try
                    {
                        return values.Values.ToList();
                    }
                    finally
                    {
                        readerWriterLock.ExitReadLock();
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
