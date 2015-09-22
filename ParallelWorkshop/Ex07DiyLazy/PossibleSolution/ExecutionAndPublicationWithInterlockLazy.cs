using System;
using System.Threading;

namespace Lurchsoft.ParallelWorkshop.Ex07DiyLazy.PossibleSolution
{
    public class ExecutionAndPublicationWithInterlockLazy<T> : ILazy<T> where T : class
    {
        private readonly Func<T> evaluator;

        private T value;
        private int state;

        public ExecutionAndPublicationWithInterlockLazy(Func<T> evaluator)
        {
            this.evaluator = evaluator;
        }

        public T Value
        {
            get
            {
                while (true)
                { 
                    int prevState = Interlocked.CompareExchange(ref state, 1, 0);
                    switch (prevState)
                    {
                        case 0:
                            T newValue = value = evaluator();
                            Volatile.Write(ref state, 2);
                            return newValue;

                        case 1:
                            Thread.Yield();
                            break;

                        case 2:
                            return value;
                    }
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
