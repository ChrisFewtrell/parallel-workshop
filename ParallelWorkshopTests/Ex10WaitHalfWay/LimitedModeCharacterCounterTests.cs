using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            const int NumConcurrent = 10;
            ITextFile[] allFiles = EmbeddedFiles.AllFiles.ToArray();
            IEnumerable<ITextFile> textFiles = Enumerable.Range(0, NumConcurrent).Select(i => allFiles[i % allFiles.Length]);

            // To solve the exercise, you may need to pass additional synchronisation information to the constructor.
            var counters = textFiles.Select(textFile => new LimitedModeCharacterCounter(textFile)).ToArray();

            // When you've succeeded, try changing this to use AsParallel(). Does it still work? It didn't for me. Explain why!
            Task[] tasks = counters.Select(c => (Task)Task.Factory.StartNew(() => c.GetCharCounts())).ToArray();
            Task.WaitAll(tasks);
        }
    }
}
