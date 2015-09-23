using System.Threading;

namespace Lurchsoft.ParallelWorkshop.Ex07DiyLazy
{
    /// <summary>
    /// A lazily-evaluated value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILazy<out T>
    {
        T Value { get; }

        /// <summary>
        /// The thread-safety level achieved by this implementation.
        /// </summary>
        LazyThreadSafetyMode Mode { get; }
    }
}