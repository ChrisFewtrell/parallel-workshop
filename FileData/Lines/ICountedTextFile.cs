namespace Lurchsoft.FileData.Lines
{
    public interface ICountedTextFile : ITextFile
    {
        int LineCount { get; }
    }
}
