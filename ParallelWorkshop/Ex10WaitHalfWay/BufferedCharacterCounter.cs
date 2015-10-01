using System;
using System.Collections.Generic;
using System.Linq;
using Lurchsoft.FileData;
using Lurchsoft.FileData.Characters;

namespace Lurchsoft.ParallelWorkshop.Ex10WaitHalfWay
{
    public class BufferedCharacterCounter : ICharacterCounter
    {
        private readonly Lazy<IReadOnlyDictionary<char, int>> charCounts;

        public BufferedCharacterCounter(ITextFile textFile)
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
            using (SystemMode.Reader)
            {
                return textFile.ReadLines().ToList();
            }
        }

        private IReadOnlyDictionary<char, int> ComputeCharCounts(IReadOnlyCollection<string> allLines)
        {
            using (SystemMode.Counter)
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
