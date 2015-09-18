using System.Collections.Generic;
using System.Linq;
using FileData;
using NUnit.Framework;

namespace ParallelWorkshop.Ex03Cache
{
    [TestFixture]
    public class CachingFileCharacterCounterTests
    {
        public static readonly IEnumerable<ITextFile> Files = EmbeddedFiles.AllFiles;

        [Test]
        public void GetCharCounts_TwiceOnSameThread_ShouldOnlyComputeOnce([ValueSource("Files")] ITextFile file)
        {
            var countedFile = new CachingFileCharacterCounter();
            countedFile.GetCharCounts(file);
            countedFile.GetCharCounts(file);
            Assert.That(countedFile.NumberOfCallsToComputeCharCounts, Is.EqualTo(1));
        }

        [Test]
        public void GetCharCounts_OnManyThreads_ShouldAlwaysGetTheSameResult([ValueSource("Files")] ITextFile file)
        {
            const int NumCalls = 10;
            var countedFile = new CachingFileCharacterCounter();
            var charCounts = Enumerable.Range(0, NumCalls).AsParallel().Select(i => countedFile.GetCharCounts(file)).ToList();
            Assert.That(charCounts.Distinct().Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetCharCounts_ShouldNotReadTheFile_UntilLineCountIsCalled()
        {
            var countedFile = new CachingFileCharacterCounter();
            Assert.That(countedFile.NumberOfCallsToComputeCharCounts, Is.EqualTo(0));
        }
    }
}
