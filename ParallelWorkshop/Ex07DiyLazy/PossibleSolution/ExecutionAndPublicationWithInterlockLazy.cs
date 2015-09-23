using System;
using System.Threading;

namespace Lurchsoft.ParallelWorkshop.Ex07DiyLazy.PossibleSolution
{
    public class ExecutionAndPublicationWithInterlockLazy<T> : ILazy<T>
    {
        private const int Uninitialised = 0, Initialising = 1, Initialised = 2; // can't do Interlocked operations on Enums :-(

        private readonly Func<T> evaluator;

        private T value;
        private int state = Uninitialised;

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
                    int prevState = Interlocked.CompareExchange(ref state, Initialising, Uninitialised);
                    switch (prevState)
                    {
                        case Uninitialised:
                            T newValue = value = evaluator();
                            Volatile.Write(ref state, Initialised);
                            return newValue;

                        case Initialising:
                            Thread.Yield();
                            break;

                        case Initialised:
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
