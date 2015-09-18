﻿using System.Linq;
using System.Threading;
using Lurchsoft.FileData;

namespace Lurchsoft.ParallelWorkshop.Ex03TotalLines.PossibleSolution
{
    public class InterlockedTotalLineCount : ITotalLineCount
    {
        private readonly ITextFile[] textFiles;
        private LineCount count;

        public InterlockedTotalLineCount(params ITextFile[] textFiles)
        {
            this.textFiles = textFiles;
        }

        public int NumberOfCallsToReadAllLines { get; private set; }

        public int GetTotalLineCount()
        {
            if (count == null)
            {
                var theCount = new LineCount { Count = ReadAllLines().Count() };
                Interlocked.CompareExchange(ref count, theCount, null);
            }

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
