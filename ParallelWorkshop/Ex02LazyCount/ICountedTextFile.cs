using FileData.Reading;

namespace ParallelWorkshop.Ex02LazyCount
{
    public interface ICountedTextFile : ITextFile
    {
        int LineCount { get; }
    }
}
