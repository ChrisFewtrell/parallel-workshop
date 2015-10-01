using System;
using System.Threading;

namespace Lurchsoft.ParallelWorkshop.Ex06DiyLazy.PossibleSolution
{
    public class ExecutionAndPublicationWithLockLazy<T> : ILazy<T> where T : class
    {
        private readonly Func<T> evaluator;
        private readonly object sync = new object();

        private T value;

        public ExecutionAndPublicationWithLockLazy(Func<T> evaluator)
        {
            this.evaluator = evaluator;
        }

        public T Value
        {
            get
            {
                // Simple and easy to understand. But every read requires acquisition of the lock - not very fast.
                lock (sync)
                {
                    return value = value ?? evaluator();
                }
            }
        }

        public LazyThreadSafetyMode Mode
        {
            get
            {
                return LazyThreadSafetyMode.ExecutionAndPublication;
            }
        }
    }
}
