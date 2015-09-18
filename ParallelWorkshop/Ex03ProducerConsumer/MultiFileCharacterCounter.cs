using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FileData;
using FileData.Characters;

namespace ParallelWorkshop.Ex03ProducerConsumer
{
    public class MultiFileCharacterCounter : ICharacterCounter, IDisposable
    {
        private const int MaxQueue = 1000;

        private readonly CharacterTotaliser totaliser = new CharacterTotaliser();
        private readonly BlockingCollection<string> textLineQueue = new BlockingCollection<string>(MaxQueue);

        public MultiFileCharacterCounter()
        {
            Task.Factory.StartNew(ConsumeQueue);
        }

        public IReadOnlyDictionary<char, int> CharCounts
        {
            get
            {
                // This will return the result at the time of call, but processing might be in progress.
                // How can we make it block until processing is complete?
                return totaliser.CharCounts;
            }
        }

        public void Add(ITextFile textFile)
        {
            if (textLineQueue.IsAddingCompleted)
            {
                throw new ObjectDisposedException("Counter has been disposed");
            }

            Task.Factory.StartNew(() => Process(textFile));
        }

        private void Process(ITextFile textFile)
        {
            foreach (string line in textFile.ReadLines())
            {
                Thread.Sleep(10);
                textLineQueue.Add(line);
            }
        }

        private void ConsumeQueue()
        {
            while (!textLineQueue.IsCompleted)
            {
                string line;
                try { line = textLineQueue.Take(); } catch (InvalidOperationException) { break; }
                totaliser.Add(line);
            }

            textLineQueue.Dispose();
        }

        public void Dispose()
        {
            textLineQueue.CompleteAdding();
        }
    }
}
