using System;
using System.Threading;
namespace Lurchsoft.ParallelWorkshop.Ex05DatedSerial.PossibleSolution
{
    public class LazyDatedSerial
    {
        private Lazy<ThreadSafeSerial> serial = new Lazy<ThreadSafeSerial>(LazyThreadSafetyMode.ExecutionAndPublication);

        public string GetNextSerial()
        {
            return serial.Value.GetNextSerial();
        }

        private class ThreadSafeSerial
        {
            private readonly string prefix;
            private int serial;

            public ThreadSafeSerial(string prefix)
            {
                this.prefix = prefix;
            }

            public string GetNextSerial()
            {
                int thisSerial = Interlocked.Increment(ref serial);
                return string.Format("{0}-{1}", prefix, thisSerial);
            }
        }
    }
}
