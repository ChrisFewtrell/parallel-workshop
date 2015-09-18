using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Lurchsoft.FileData;

namespace Lurchsoft.ParallelWorkshop.Ex02LineCount
{
    public class CountedTextFile : ICountedTextFile
    {
        private readonly ITextFile textFile;

        private int numberOfFileReads;

        public CountedTextFile(ITextFile textFile)
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
                // This is inefficient, if called multiple times.
                //
                // How can we ensure that the file is only read once?
                //
                // In this example, make sure it is read only a total of once, on all calling threads.
                //
                // Try at least the following solutions: -
                // - Lazy<T>
                // - LazyInitializer<T>
                return CountLines();
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
