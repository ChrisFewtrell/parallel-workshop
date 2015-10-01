using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lurchsoft.FileData;
using Lurchsoft.ParallelWorkshop.Ex10WaitHalfWay;
using NUnit.Framework;

namespace Lurchsoft.ParallelWorkshopTests.Ex10WaitHalfWay
{
    [TestFixture]
    public class LimitedModeCharacterCounterTests
    {
        [Test]
        public void GetCharCounts_ShouldSucceed_WhenSeveralInstancesRunConcurrently()
        {
            LimitedModeCharacterCounter[] counters = CreateCounters();
            Task[] tasks = counters.Select(c => (Task)Task.Factory.StartNew(() => c.GetCharCounts())).ToArray();
            Task.WaitAll(tasks);
        }

        [Test, Explicit("This will probably hang, once you make it use your Barrier-based solution. Can you work out why?")]
        public void GetCharCounts_UsingAsParallel()
        {
            LimitedModeCharacterCounter[] counters = CreateCounters();
            var results = counters.AsParallel().Select(c => c.GetCharCounts()).ToArray();
            Assert.That(results, Is.Not.Empty);
        }

        private static LimitedModeCharacterCounter[] CreateCounters()
        {
            const int NumConcurrent = 10;
            ITextFile[] allFiles = EmbeddedFiles.AllFiles.ToArray();
            IEnumerable<ITextFile> textFiles = Enumerable.Range(0, NumConcurrent).Select(i => allFiles[i % allFiles.Length]);

            // To solve the exercise, you may need to pass additional synchronisation information to the constructor.
            var counters = textFiles.Select(textFile => new LimitedModeCharacterCounter(textFile)).ToArray();
            return counters;
        }
    }
}
