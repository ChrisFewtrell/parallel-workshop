using FileData.Reading;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ParallelWorkshop.Ex02LazyCount
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
                // How can we ensure that the file is only read once?
                // Don't use a lock, but there are still a few choices of how to do it.
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
