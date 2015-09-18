using System.Threading;

namespace Lurchsoft.ParallelWorkshop.Ex01Serial.PossibleSolution
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
