namespace Lurchsoft.ParallelWorkshop.Ex07DiyLazy
{
    /// <summary>
    /// A lazily-evaluated value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILazy<out T>
    {
        T Value { get; }
    }
}