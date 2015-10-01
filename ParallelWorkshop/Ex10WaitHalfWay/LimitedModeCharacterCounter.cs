using System;
using System.Collections.Generic;
using System.Linq;
using Lurchsoft.FileData;
using Lurchsoft.FileData.Characters;

namespace Lurchsoft.ParallelWorkshop.Ex10WaitHalfWay
{
    /// <summary>
    /// A character counter that simulates a situation where information is coming from a system that can only
    /// be either reading files or counting characters. If one thread tries to read while another counts, it
    /// will fail. This limitation is simulated by <see cref="SystemMode"/>.
    /// </summary>
    public class LimitedModeCharacterCounter : ICharacterCounter
    {
        private readonly Lazy<IReadOnlyDictionary<char, int>> charCounts;

        public LimitedModeCharacterCounter(ITextFile textFile)
        {
            charCounts = new Lazy<IReadOnlyDictionary<char, int>>(() => BufferAndCount(textFile));
        }

        public IReadOnlyDictionary<char, int> GetCharCounts()
        {
            return charCounts.Value;
        }

        private IReadOnlyDictionary<char, int> BufferAndCount(ITextFile textFile)
        {
            IReadOnlyCollection<string> allLines = ReadAllLines(textFile);
            return ComputeCharCounts(allLines);
        }

        private static IReadOnlyCollection<string> ReadAllLines(ITextFile textFile)
        {
            using (SystemMode.Reader) // don't remove this - that's cheating!
            {
                return textFile.ReadLines().ToList();
            }
        }

        private IReadOnlyDictionary<char, int> ComputeCharCounts(IReadOnlyCollection<string> allLines)
        {
            using (SystemMode.Counter) // don't remove this - that's cheating!
            {
                var totaliser = new CharacterTotaliser();
                foreach (string line in allLines)
                {
                    totaliser.Add(line);
                }

                return totaliser.GetCharCounts();
            }
        }
    }
}
