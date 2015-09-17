using System.Collections.Generic;
using System.Linq;
using FileData;
using FileData.Reading;
using NUnit.Framework;

namespace ParallelWorkshop.Ex02LazyCount
{
    [TestFixture]
    public class CountedTextFileTests
    {
        public static readonly IEnumerable<ITextFile> Files = EmbeddedFiles.AllFiles;

        [Test]
        public void LineCount_OnManyThreads_ShouldOnlyReadFileOnce([ValueSource("Files")] ITextFile file)
        {
            const int NumCalls = 10;
            var countedFile = new CountedTextFile(file);
            var lineCounts = Enumerable.Range(0, NumCalls).AsParallel().Select(i => countedFile.LineCount).ToList();
            Assert.That(lineCounts.Distinct().Count(), Is.EqualTo(1), "Inconsistent results obtained");
            Assert.That(countedFile.NumberOfCallsToCountLines, Is.EqualTo(1), "Inefficient - read the file many times");
        }
    }
}
