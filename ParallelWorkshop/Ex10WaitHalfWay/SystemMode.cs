using System;
using System.Threading;
using Lurchsoft.FileData.Util;

namespace Lurchsoft.ParallelWorkshop.Ex10WaitHalfWay
{
    /// <summary>
    /// This models a system that can only do one kind of operation, over all threads. Specifically, it can
    /// either be idle, reading or counting. If one thread tries to read while another is counting, or 
    /// vice-versa, it will throw.
    /// </summary>
    public static class SystemMode
    {
        // I'm doing smart-ass lock-free state transitions here. I don't necessarily suggest this
        // as good practice in production code. For instance, there's a nasty implicit limit here...
        private const int OneRead = 0x001;
        private const int ReadMask = 0xFFF;
        private const int OneCount = 0x1000;
        private const int CountMask = 0xFFF000;

        private static int state;

        public static IDisposable Reader
        {
            get { return new StartStop(StartReading, StopReading); }
        }

        public static IDisposable Counter
        {
            get { return new StartStop(StartCounting, StopCounting); }
        }

        private static void StartReading()
        {
            int prev = Interlocked.Add(ref state, OneRead);
            if ((prev & CountMask) != 0)
            {
                throw new InvalidOperationException("Can't start reading while counting");
            }
        }

        private static void StopReading()
        {
            Interlocked.Add(ref state, -OneRead);
        }

        private static void StartCounting()
        {
            int prev = Interlocked.Add(ref state, OneCount);
            if ((prev & ReadMask) != 0)
            {
                throw new InvalidOperationException("Can't start counting while reading");
            }
        }

        private static void StopCounting()
        {
            Interlocked.Add(ref state, -OneCount);
        }
    }
}
