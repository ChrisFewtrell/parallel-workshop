using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lurchsoft.FileData;
using Lurchsoft.FileData.Characters;

namespace Lurchsoft.ParallelWorkshop.Ex09ProducerConsumer
{
    /// <summary>
    /// This uses <see cref="BlockingCollection{T}"/> to implement a producer-consumer pattern using very
    /// little code.
    /// <para>
    /// The main thing that you should do in this exercise is to understand how the producer-consumer pattern
    /// is implemented here.
    /// </para>
    /// <para>
    /// The coding exercise is something of a side-show. To make the tests go green, you'll need to add some
    /// extra co-ordination logic to allow an external class to make a call to get queue results while it's still "live".</para>
    /// </summary>
    public class MultiFileCharacterCounter : IDisposable, IMultiFileCharacterCounter
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
                if (textLineQueue.TryTake(out line))
                {
                    totaliser.Add(line);
                }
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
