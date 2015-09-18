using System.Collections.Concurrent;
using Lurchsoft.FileData;
using Lurchsoft.FileData.Characters;

namespace Lurchsoft.ParallelWorkshop.Ex04ItemCache.SolutionWithConcurrentCollection
{
    public class CachingFileCharacterCounter : IFileCharacterCounter
    {
        private readonly ConcurrentDictionary<ITextFile, ICharacterCounter> cache = new ConcurrentDictionary<ITextFile, ICharacterCounter>();

        public int NumberOfCallsToComputeCharCounts { get; private set; }

        public ICharacterCounter GetCharCounts(ITextFile textFile)
        {
            // Concise, huh? It's almost as if ConcurrentDictionary was designed for this...
            return cache.GetOrAdd(textFile, ComputeCharCounts);
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
