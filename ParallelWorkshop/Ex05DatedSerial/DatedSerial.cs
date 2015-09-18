using System;
using System.Threading;
namespace Lurchsoft.ParallelWorkshop.Ex05DatedSerial
{
    public class DatedSerial
    {
        private ThreadSafeSerial serial;

        public string GetNextSerial()
        {
            // ThreadSafeSerial is thread-safe, so we're good, yes?
            //
            // No, because we may get more than one instance of it!
            //
            // You need to ensure that we only get one instance. Try at least the following: -
            // * Lazy<T> - experiment with the LazyThreadSafetyMode
            // * LazyInitializer<T> - look at the different signatures
            //
            // The above will use a lock internally. Can you avoid it? I haven't managed to!
            serial = serial ?? new ThreadSafeSerial(DateTime.Now.ToShortDateString());
            return serial.GetNextSerial();
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
