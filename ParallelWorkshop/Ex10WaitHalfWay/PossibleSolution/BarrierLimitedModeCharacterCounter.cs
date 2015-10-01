using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Lurchsoft.FileData;
using Lurchsoft.FileData.Characters;

namespace Lurchsoft.ParallelWorkshop.Ex10WaitHalfWay.PossibleSolution
{
    public class BarrierLimitedModeCharacterCounter : ICharacterCounter
    {
        private readonly Barrier barrier;
        private readonly Lazy<IReadOnlyDictionary<char, int>> charCounts;

        public BarrierLimitedModeCharacterCounter(ITextFile textFile, Barrier barrier)
        {
            this.barrier = barrier;
            barrier.AddParticipant();

            charCounts = new Lazy<IReadOnlyDictionary<char, int>>(() => BufferAndCount(textFile));
        }

        public IReadOnlyDictionary<char, int> GetCharCounts()
        {
            return charCounts.Value;
        }

        private IReadOnlyDictionary<char, int> BufferAndCount(ITextFile textFile)
        {
            IReadOnlyCollection<string> allLines = ReadAllLines(textFile);
            barrier.SignalAndWait();
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
