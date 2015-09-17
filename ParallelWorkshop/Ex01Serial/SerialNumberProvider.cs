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
            return ++curNumber;
        }
    }
}
