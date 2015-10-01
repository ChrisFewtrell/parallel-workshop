using System;

namespace Lurchsoft.FileData.Util
{
    public class StartStop : IDisposable
    {
        private readonly Action stop;

        public StartStop(Action start, Action stop)
        {
            this.stop = stop;
            start();
        }

        public void Dispose()
        {
            stop();
        }
    }
}