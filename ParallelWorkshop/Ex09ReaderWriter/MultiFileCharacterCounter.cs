using System.Collections.Generic;
using Lurchsoft.FileData;
using Lurchsoft.FileData.Characters;

namespace Lurchsoft.ParallelWorkshop.Ex09ReaderWriter
{
    public class MultiFileCharacterCounter : IMultiFileCharacterCounter
    {
        private readonly CharacterTotaliser totaliser = new CharacterTotaliser();

        public IReadOnlyDictionary<char, int> GetCharCounts()
        {
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
