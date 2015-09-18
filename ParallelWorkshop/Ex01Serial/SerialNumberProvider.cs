namespace Lurchsoft.ParallelWorkshop.Ex01Serial
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
