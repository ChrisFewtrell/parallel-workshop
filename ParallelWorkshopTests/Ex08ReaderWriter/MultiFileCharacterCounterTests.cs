using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lurchsoft.FileData;
using Lurchsoft.ParallelWorkshop.Ex08ReaderWriter;
using NUnit.Framework;

namespace Lurchsoft.ParallelWorkshopTests.Ex08ReaderWriter
{
    [TestFixture]
    public class MultiFileCharacterCounterTests
    {
        [Test]
        public void GetCharCounts_ShouldReturnDoubledValues_WhenSameFileAddedSecondTime()
        {
            var counter = new MultiFileCharacterCounter();
            counter.Add(EmbeddedFiles.Large);
            var firstResults = counter.GetCharCounts();
            counter.Add(EmbeddedFiles.Large);
            Assert.That(counter.GetCharCounts().All(secondResult => IsDoubled(secondResult, firstResults)));
        }

        [Test]
        public void GetCharCounts_ShouldReturnValue_WhenCalledWhileAddsAreOccurringOnOtherThreads()
        {
            const int NumAdds = 1000, NumGets = 10000;
            var counter = new MultiFileCharacterCounter();
            Task[] addTasks = Enumerable.Range(0, NumAdds).Select(i => Task.Factory.StartNew(() => counter.Add(EmbeddedFiles.Medium))).ToArray();
            var results = Enumerable.Range(0, NumGets).Select(i => counter.GetCharCounts()).ToList();
            Assert.That(results.All(d => d != null));
            Task.WaitAll(addTasks);
        }

        private static bool IsDoubled(KeyValuePair<char, int> secondResult, IReadOnlyDictionary<char, int> firstResults)
        {
            return secondResult.Value == firstResults[secondResult.Key] * 2;
        }
    }
}
