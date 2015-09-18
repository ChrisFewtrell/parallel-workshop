using System.Collections.Generic;
using System.Linq;
using FileData;
using NUnit.Framework;

namespace ParallelWorkshop.Ex02LineCount
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
            Enumerable.Range(0, NumCalls).AsParallel().Select(i => countedFile.LineCount).ToList();
            Assert.That(countedFile.NumberOfCallsToCountLines, Is.EqualTo(1));
        }

        [Test]
        public void LineCount_OnManyThreads_ShouldAlwaysGetTheSameResult([ValueSource("Files")] ITextFile file)
        {
            const int NumCalls = 10;
            var countedFile = new CountedTextFile(file);
            var lineCounts = Enumerable.Range(0, NumCalls).AsParallel().Select(i => countedFile.LineCount).ToList();
            Assert.That(lineCounts.Distinct().Count(), Is.EqualTo(1));
        }

        [Test]
        public void LineCount_ShouldNotReadTheFile_UntilLineCountIsCalled()
        {
            var countedFile = new CountedTextFile(EmbeddedFiles.Medium);
            Assert.That(countedFile.NumberOfCallsToCountLines, Is.EqualTo(0));
        }
    }
}
