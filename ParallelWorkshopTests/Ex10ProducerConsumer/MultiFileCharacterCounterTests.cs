using System.Collections.Generic;
using System.Threading;
using Lurchsoft.FileData;
using Lurchsoft.ParallelWorkshop.Ex10ProducerConsumer;
using NUnit.Framework;

namespace Lurchsoft.ParallelWorkshopTests.Ex10ProducerConsumer
{
    [TestFixture]
    public class MultiFileCharacterCounterTests
    {
        [Test]
        public void GetCharCounts_ShouldReturnTheFinalCountForAllAddedFiles()
        {
            using (var counter = new MultiFileCharacterCounter())
            {
                counter.Add(EmbeddedFiles.Medium);
                counter.Add(EmbeddedFiles.Large);
                counter.Add(EmbeddedFiles.Huge);

                IReadOnlyDictionary<char, int> result1 = counter.GetCharCounts();
                Assert.That(result1, Is.Not.Empty);

                Thread.Sleep(250);

                IReadOnlyDictionary<char, int> result2 = counter.GetCharCounts();
                Assert.That(result2, Is.Not.Empty);

                Assert.That(result2, Is.EqualTo(result1));
            }
        }

        [Test]
        public void GetCharCounts_ShouldReturnRevisedCountWhenAnotherFileAddedAfterGettingInitialResults()
        {
            using (var counter = new MultiFileCharacterCounter())
            {
                counter.Add(EmbeddedFiles.Large);
                counter.Add(EmbeddedFiles.Huge);

                Thread.Sleep(250);
                IReadOnlyDictionary<char, int> result1 = counter.GetCharCounts();

                counter.Add(EmbeddedFiles.Medium);

                Thread.Sleep(250);
                IReadOnlyDictionary<char, int> result2 = counter.GetCharCounts();

                Assert.That(result2.Count, Is.GreaterThan(result1.Count));
            }
        }
    }
}
