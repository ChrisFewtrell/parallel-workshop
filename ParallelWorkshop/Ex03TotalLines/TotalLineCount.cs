using System.Collections.Generic;
using System.Linq;
using Lurchsoft.FileData;

namespace Lurchsoft.ParallelWorkshop.Ex03TotalLines
{
    public class TotalLineCount : ITotalLineCount
    {
        private readonly ITextFile[] textFiles;

        public TotalLineCount(params ITextFile[] textFiles)
        {
            this.textFiles = textFiles;
        }

        public int NumberOfCallsToReadAllLines { get; private set; }

        public int GetTotalLineCount()
        {
            // This is inefficient, if called multiple times.
            //
            // How can we ensure that the files are only read once?
            //
            // In this example, do NOT prevent the files being read again, on a different calling thread.
            //
            // Try at least the following solutions: -
            // - Lazy<T>
            // - LazyInitializer<T>
            // - Interlocked.CompareExchange()
            return ReadAllLines().Count();
        }

        private ParallelQuery<string> ReadAllLines()
        {
            ++NumberOfCallsToReadAllLines;
            return textFiles.AsParallel().SelectMany(tf => tf.ReadLines());
        }
    }
}
