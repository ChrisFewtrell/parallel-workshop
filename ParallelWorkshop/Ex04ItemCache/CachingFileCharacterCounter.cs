using System.Collections.Generic;
using Lurchsoft.FileData;
using Lurchsoft.FileData.Characters;

namespace Lurchsoft.ParallelWorkshop.Ex04ItemCache
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
