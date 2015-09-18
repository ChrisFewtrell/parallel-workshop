using System.Linq;
using System.Threading;
using Lurchsoft.FileData;
using Lurchsoft.FileData.Lines;

namespace Lurchsoft.ParallelWorkshop.Ex03TotalLines.PossibleSolution
{
    public class LazyInitialisedTotalLineCount : ITotalLineCount
    {
        private readonly ITextFile[] textFiles;
        private LineCount count;

        public LazyInitialisedTotalLineCount(params ITextFile[] textFiles)
        {
            this.textFiles = textFiles;
        }

        public int NumberOfCallsToReadAllLines { get; private set; }

        public int GetTotalLineCount()
        {
            LazyInitializer.EnsureInitialized(ref count, () => new LineCount { Count = ReadAllLines().Count() });
            return count.Count;
        }

        private ParallelQuery<string> ReadAllLines()
        {
            ++NumberOfCallsToReadAllLines;
            return textFiles.AsParallel().SelectMany(tf => tf.ReadLines());
        }

        private class LineCount { public int Count; }
    }
}
