using System.Collections.Generic;
using System.Threading;
using Lurchsoft.FileData;
using Lurchsoft.FileData.Characters;

namespace Lurchsoft.ParallelWorkshop.Ex07ReaderWriter.PossibleSolution
{
    public class ReaderWriterMultiFileCharacterCounter : IMultiFileCharacterCounter
    {
        private readonly ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        private readonly CharacterTotaliser totaliser = new CharacterTotaliser();

        public IReadOnlyDictionary<char, int> GetCharCounts()
        {
            readerWriterLock.EnterReadLock();
            try
            {
                return totaliser.GetCharCounts();
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        public void Add(ITextFile textFile)
        {
            // We could possibly put the lock around just Add(), but would then incur a lot of lock-entry/exit
            // time. It could sometimes be better, but this way probably wins most times.
            readerWriterLock.EnterWriteLock();
            try
            {
                foreach (string line in textFile.ReadLines())
                {
                    totaliser.Add(line);
                }
            }
            finally
            {
                readerWriterLock.ExitWriteLock();
            }
        }
    }
}
