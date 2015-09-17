using FileData.Reading;

namespace ParallelWorkshop.Ex02LineCount
{
    public interface ICountedTextFile : ITextFile
    {
        int LineCount { get; }
    }
}
