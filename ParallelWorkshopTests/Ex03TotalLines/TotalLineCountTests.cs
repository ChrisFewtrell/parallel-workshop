using System.Linq;
using Lurchsoft.FileData;
using Lurchsoft.ParallelWorkshop.Ex03TotalLines;
using NUnit.Framework;

namespace Lurchsoft.ParallelWorkshopTests.Ex03TotalLines
{
    [TestFixture]
    public class TotalLineCountTests
    {
        [Test]
        public void GetTotalLineCount_ShouldOnlyReadEachFileOnce_WhenCalledTwiceOnSameThread()
        {
            var totaliser = new TotalLineCount(EmbeddedFiles.Huge, EmbeddedFiles.Large, EmbeddedFiles.Medium);
            totaliser.GetTotalLineCount();
            totaliser.GetTotalLineCount();
            Assert.That(totaliser.NumberOfCallsToReadAllLines, Is.EqualTo(1));
        }

        [Test]
        public void GetTotalLineCount_ShouldReturnSameValue_WhenCalledOnMultipleThreads()
        {
            const int NumCalls = 50;
            var totaliser = new TotalLineCount(EmbeddedFiles.Huge, EmbeddedFiles.Large, EmbeddedFiles.Medium);
            var results = Enumerable.Range(0, NumCalls).AsParallel().Select(i => totaliser.GetTotalLineCount()).ToList();
            Assert.That(results.Distinct().Count(), Is.EqualTo(1));
        }
    }
}
