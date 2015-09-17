using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelWorkshop.Ex01Serial.SolutionWithInterlocked
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
