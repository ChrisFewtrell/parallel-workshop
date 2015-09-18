using System;
using System.Linq;
using System.Threading;
using Lurchsoft.FileData;
using Lurchsoft.FileData.Lines;

namespace Lurchsoft.ParallelWorkshop.Ex03TotalLines.PossibleSolution
{
    public class LazyTotalLineCount : ITotalLineCount
    {
        private readonly ITextFile[] textFiles;
        private readonly Lazy<int> lazyCount;

        public LazyTotalLineCount(params ITextFile[] textFiles)
        {
            this.textFiles = textFiles;
            lazyCount = new Lazy<int>(() => ReadAllLines().Count(), LazyThreadSafetyMode.PublicationOnly);
        }

        public int NumberOfCallsToReadAllLines { get; private set; }

        public int GetTotalLineCount()
        {
            return lazyCount.Value;
        }

        private ParallelQuery<string> ReadAllLines()
        {
            ++NumberOfCallsToReadAllLines;
            return textFiles.AsParallel().SelectMany(tf => tf.ReadLines());
        }
    }
}
