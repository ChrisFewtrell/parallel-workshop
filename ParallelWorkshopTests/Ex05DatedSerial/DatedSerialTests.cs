using System.Linq;
using Lurchsoft.ParallelWorkshop.Ex05DatedSerial;
using NUnit.Framework;

namespace Lurchsoft.ParallelWorkshopTests.Ex05DatedSerial
{
    [TestFixture]
    public class DatedSerialTests
    {
        [Test]
        public void GetNextSerial_ShouldReturnUniqueSerialNumbers_WhenCalledOnManyThreads()
        {
            const int NumCalls = 10000;
            var serial = new DatedSerial();
            var results = Enumerable.Range(0, NumCalls).AsParallel().Select(i => serial.GetNextSerial()).ToList();
            Assert.That(results.Distinct().Count(), Is.EqualTo(NumCalls));
        }
    }
}
