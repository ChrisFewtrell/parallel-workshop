namespace Lurchsoft.FileData.Characters
{
    public interface IFileCharacterCounter
    {
        ICharacterCounter GetCharCounts(ITextFile textFile);
    }
}
