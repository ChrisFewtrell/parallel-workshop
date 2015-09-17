using FileData;

namespace ParallelWorkshop.Ex02LineCount
{
    public interface ICountedTextFile : ITextFile
    {
        int LineCount { get; }
    }
}
