using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Lurchsoft.FileData;

namespace Lurchsoft.ParallelWorkshop.Ex02LineCount.PossibleSolution
{
    public class CountedTextFile : ICountedTextFile
    {
        private readonly ITextFile textFile;
        private readonly Lazy<int> lazyCount;

        private int numberOfFileReads;

        public CountedTextFile(ITextFile textFile)
        {
            this.textFile = textFile;
            lazyCount = new Lazy<int>(CountLines, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public IEnumerable<string> ReadLines()
        {
            return textFile.ReadLines();
        }

        public int LineCount
        {
            get { return lazyCount.Value; }
        }

        public int NumberOfCallsToCountLines
        {
            get { return numberOfFileReads; }
        }

        private int CountLines()
        {
            Interlocked.Increment(ref numberOfFileReads);
            return ReadLines().Count();
        }
    }
}
