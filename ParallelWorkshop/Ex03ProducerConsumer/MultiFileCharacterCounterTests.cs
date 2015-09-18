using System.Collections.Generic;
using System.Threading;
using FileData;
using NUnit.Framework;

namespace ParallelWorkshop.Ex03ProducerConsumer
{
    [TestFixture]
    public class MultiFileCharacterCounterTests
    {
        [Test]
        public void CharCounts_ShouldReturnTheCountForAllAddedFiles()
        {
            using (var counter = new MultiFileCharacterCounter())
            {
                counter.Add(EmbeddedFiles.Medium);
                counter.Add(EmbeddedFiles.Large);
                counter.Add(EmbeddedFiles.Huge);

                IReadOnlyDictionary<char, int> result1 = counter.GetCharCounts();
                Assert.That(result1, Is.Not.Empty);

                Thread.Sleep(100);

                IReadOnlyDictionary<char, int> result2 = counter.GetCharCounts();
                Assert.That(result2, Is.Not.Empty);

                Assert.That(result2, Is.EqualTo(result1));
            }
        }
    }
}
