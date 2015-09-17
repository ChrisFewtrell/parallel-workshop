using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelWorkshop.Ex01Serial
{
    public class SerialNumberProvider : ISerialNumberProvider
    {
        private int curNumber;

        public int GetNextSerialNumber()
        {
            // This isn't thread-safe. What's the best way to make it so?
            return ++curNumber;
        }
    }
}
