using Lurchsoft.FileData;

namespace Lurchsoft.ParallelWorkshop.Ex02LineCount
{
    public interface ICountedTextFile : ITextFile
    {
        int LineCount { get; }
    }
}
