using System.Collections.Generic;
using Lurchsoft.FileData;
using Lurchsoft.FileData.Characters;

namespace Lurchsoft.ParallelWorkshop.Ex07ReaderWriter
{
    public class MultiFileCharacterCounter : IMultiFileCharacterCounter
    {
        private readonly CharacterTotaliser totaliser = new CharacterTotaliser();

        public IReadOnlyDictionary<char, int> GetCharCounts()
        {
            // This will throw if someone adds a line to the totaliser while it's executing.
            // We need to make it thread-safe and we want to allow multiple simultaneous reads.
            //
            // Try at least the following approaches: -
            // - ReaderWriterLock
            // - ReaderWriterLockSlim
            return totaliser.GetCharCounts();
        }

        public void Add(ITextFile textFile)
        {
            foreach (string line in textFile.ReadLines())
            {
                totaliser.Add(line);
            }
        }
    }
}
