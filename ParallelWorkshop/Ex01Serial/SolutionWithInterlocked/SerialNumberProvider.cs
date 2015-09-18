using System.Threading;

namespace Lurchsoft.ParallelWorkshop.Ex01Serial.SolutionWithInterlocked
{
    public class SerialNumberProvider : ISerialNumberProvider
    {
        private int curNumber;

        public int GetNextSerialNumber()
        {
            return Interlocked.Increment(ref curNumber);
        }
    }
}
