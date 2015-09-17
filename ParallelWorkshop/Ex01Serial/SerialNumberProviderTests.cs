using NUnit.Framework;
using System.Linq;

namespace ParallelWorkshop.Ex01Serial
{
    [TestFixture]
    public class SerialNumberProviderTests
    {
        [Test]
        public void GetNextSerialNumber_ShouldReturnUniqueValues_WhenCalledOnManyThreads()
        {
            const int Count = 1000000;
            var provider = new SerialNumberProvider();
            var serials = Enumerable.Range(0, Count).AsParallel().Select(i => provider.GetNextSerialNumber()).ToList();
            var uniques = serials.Distinct().ToList();
            Assert.That(uniques.Count, Is.EqualTo(serials.Count));
        }
    }
}
