using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Lurchsoft.FileData;

namespace Lurchsoft.ParallelWorkshop.Ex02LineCount.PossibleSolution
{
    public class LazyInitialisedCountedTextFile : ICountedTextFile
    {
        private readonly ITextFile textFile;

        private int lazyCount;
        private bool initialised;
        private object sync = new object();

        private int numberOfFileReads;

        public LazyInitialisedCountedTextFile(ITextFile textFile)
        {
            this.textFile = textFile;
        }

        public IEnumerable<string> ReadLines()
        {
            return textFile.ReadLines();
        }

        public int LineCount
        {
            get 
            {
                // In this situation, LazyInitializer is ugly. In others, it's the right way.
                LazyInitializer.EnsureInitialized(ref lazyCount, ref initialised, ref sync, CountLines);
                return lazyCount;
            }
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
