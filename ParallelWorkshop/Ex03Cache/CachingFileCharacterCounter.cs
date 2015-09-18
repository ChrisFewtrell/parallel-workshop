using System.Collections.Generic;
using FileData;
using FileData.Characters;

namespace ParallelWorkshop.Ex03Cache
{
    public class CachingFileCharacterCounter : IFileCharacterCounter
    {
        private readonly IDictionary<ITextFile, ICharacterCounter> cache = new Dictionary<ITextFile, ICharacterCounter>();

        public int NumberOfCallsToComputeCharCounts { get; private set; }

        public ICharacterCounter GetCharCounts(ITextFile textFile)
        {
            ICharacterCounter characterCounter;
            if (!cache.TryGetValue(textFile, out characterCounter))
            {
                cache.Add(textFile, characterCounter = ComputeCharCounts(textFile));
            }

            return characterCounter;
        }

        private ICharacterCounter ComputeCharCounts(ITextFile textFile)
        {
            ++NumberOfCallsToComputeCharCounts;
            var counter = new CharacterTotaliser();
            foreach (string line in textFile.ReadLines())
            {
                counter.Add(line);
            }

            return counter;
        }
    }
}
