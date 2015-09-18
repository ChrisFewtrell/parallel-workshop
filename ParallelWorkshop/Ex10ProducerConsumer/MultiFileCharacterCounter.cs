using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lurchsoft.FileData;
using Lurchsoft.FileData.Characters;

namespace Lurchsoft.ParallelWorkshop.Ex10ProducerConsumer
{
    public class MultiFileCharacterCounter : ICharacterCounter, IDisposable
    {
        private const int MaxQueue = 1000;

        private readonly CharacterTotaliser totaliser = new CharacterTotaliser();
        private readonly BlockingCollection<string> textLineQueue = new BlockingCollection<string>(MaxQueue);
        private readonly Task consumerTask;

        public MultiFileCharacterCounter()
        {
            consumerTask = Task.Factory.StartNew(ConsumeQueue);
        }

        public IReadOnlyDictionary<char, int> GetCharCounts()
        {
            // This will return the result at the time of call, but processing might be in progress.
            // How can we make it block until processing is complete?
            return totaliser.GetCharCounts();
        }

        public void Add(ITextFile textFile)
        {
            Task.Factory.StartNew(() => Process(textFile));
        }

        private void Process(ITextFile textFile)
        {
            foreach (string line in textFile.ReadLines())
            {
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
        }

        public void Dispose()
        {
            textLineQueue.CompleteAdding();
            try
            {
                consumerTask.Wait();
            }
            finally
            {
                textLineQueue.Dispose();
            }
        }
    }
}
