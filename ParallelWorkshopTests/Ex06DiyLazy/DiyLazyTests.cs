using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Lurchsoft.FileData;
using Lurchsoft.FileData.Lines;
using Lurchsoft.ParallelWorkshop.Ex06DiyLazy;
using NUnit.Framework;

namespace Lurchsoft.ParallelWorkshopTests.Ex06DiyLazy
{
    [TestFixture]
    public class DiyLazyTests
    {
        public static readonly IEnumerable<ITextFile> Files = EmbeddedFiles.AllFiles;

        [Test]
        public void LineCount_ShouldNotReadTheFile_UntilLineCountIsCalled()
        {
            var countedFile = new CountedTextFile(EmbeddedFiles.Medium, f => new DiyLazy<FileLineCount>(f));
            Assert.That(countedFile.NumberOfCallsToCountLines, Is.EqualTo(0));
        }

        [Test]
        public void LineCount_OnManyThreads_ShouldAlwaysGetTheSameResult([ValueSource("Files")] ITextFile file)
        {
            const int NumCalls = 10;
            var countedFile = new CountedTextFile(file, f => new DiyLazy<FileLineCount>(f));
            var lineCounts = Enumerable.Range(0, NumCalls).AsParallel().Select(i => countedFile.LineCount).ToList();
            Assert.That(lineCounts.Distinct().Count(), Is.EqualTo(1));
        }

        [Test]
        public void LineCount_OnManyThreads_ShouldOnlyReadFileOnce([ValueSource("Files")] ITextFile file)
        {
            const int NumCalls = 10;
            ILazy<FileLineCount> lazy = null;
            var countedFile = new CountedTextFile(file, f => lazy = new DiyLazy<FileLineCount>(f));
            Enumerable.Range(0, NumCalls).AsParallel().Select(i => countedFile.LineCount).ToList();

            switch (lazy.Mode)
            {
                case LazyThreadSafetyMode.PublicationOnly:
                    Assert.Ignore("This lazy does not guarantee only one execution, on all threads - not an error");
                    break;
                case LazyThreadSafetyMode.ExecutionAndPublication:
                    Assert.That(countedFile.NumberOfCallsToCountLines, Is.EqualTo(1));
                    break;
            }
        }

        private class CountedTextFile : ICountedTextFile
        {
            private readonly ITextFile textFile;
            private readonly ILazy<FileLineCount> lazyCount;

            private int numberOfFileReads;

            public CountedTextFile(ITextFile textFile, Func<Func<FileLineCount>, ILazy<FileLineCount>> makeLazy)
            {
                this.textFile = textFile;
                lazyCount = makeLazy(CountLines);
            }

            public IEnumerable<string> ReadLines()
            {
                return textFile.ReadLines();
            }

            public int LineCount { get { return lazyCount.Value.Count; } }

            public int NumberOfCallsToCountLines { get { return numberOfFileReads; } }

            private FileLineCount CountLines()
            {
                Interlocked.Increment(ref numberOfFileReads);
                return new FileLineCount { Count =ReadLines().Count() };
            }
        }

        private class FileLineCount
        {
            public int Count { get; set; }
        }
    }
}
