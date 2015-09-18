using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lurchsoft.FileData;
using Lurchsoft.FileData.Characters;

namespace Lurchsoft.ParallelWorkshop.Ex10ProducerConsumer.PossibleSolution
{
    /// <summary>
    /// This version passes the tests (for me, at least), but I am not sure if it is either 100% correct or
    /// the simplest possible solution. Can you do better?
    /// </summary>
    public class BlockingMultiFileCharacterCounter : IMultiFileCharacterCounter, IDisposable
    {
        private const int MaxQueue = 1000;

        private readonly CharacterTotaliser totaliser = new CharacterTotaliser();
        private readonly BlockingCollection<string> textLineQueue = new BlockingCollection<string>(MaxQueue);
        private readonly ConcurrentDictionary<Task, ITextFile> producerTasks = new ConcurrentDictionary<Task, ITextFile>();
        private readonly Task consumerTask;
        private readonly ManualResetEventSlim emptyEvent = new ManualResetEventSlim();

        public BlockingMultiFileCharacterCounter()
        {
            consumerTask = Task.Factory.StartNew(ConsumeQueue);
        }

        public IReadOnlyDictionary<char, int> GetCharCounts()
        {
            while (textLineQueue.Count > 0 || producerTasks.Count > 0)
            {
                emptyEvent.Wait();
            }

            return totaliser.GetCharCounts();
        }

        public void Add(ITextFile textFile)
        {
            Task task = new Task(() => ProduceFrom(textFile));
            task.ContinueWith(HandleFileProcessed);
            producerTasks.TryAdd(task, textFile);
            task.Start();
        }

        private void HandleFileProcessed(Task task)
        {
            ITextFile textFile;
            producerTasks.TryRemove(task, out textFile);
            emptyEvent.Set();
        }

        private void ProduceFrom(ITextFile textFile)
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
                else
                {
                    emptyEvent.Set();
                    try { line = textLineQueue.Take(); } catch (InvalidOperationException) { break; }
                }

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
