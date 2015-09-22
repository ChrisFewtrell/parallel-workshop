using System;
using System.Threading;

namespace Lurchsoft.ParallelWorkshop.Ex07DiyLazy.PossibleSolution
{
    public class PublicationOnlyLazy<T> : ILazy<T> where T : class
    {
        private readonly Func<T> evaluator;

        private T value;

        public PublicationOnlyLazy(Func<T> evaluator)
        {
            this.evaluator = evaluator;
        }

        public T Value
        {
            get
            {
                T curValue = Volatile.Read(ref value);
                if (curValue != null)
                {
                    return curValue;
                }

                T newValue = evaluator();
                T prevValue = Interlocked.CompareExchange(ref value, newValue, null);
                return prevValue ?? newValue;
            }
        }

        public LazyThreadSafetyMode Mode
        {
            get
            {
                return LazyThreadSafetyMode.PublicationOnly;
            }
        }
    }
}
