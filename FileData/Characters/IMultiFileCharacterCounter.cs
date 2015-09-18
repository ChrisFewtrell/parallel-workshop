namespace Lurchsoft.FileData.Characters
{
    public interface IMultiFileCharacterCounter : ICharacterCounter
    {
        void Add(ITextFile textFile);
    }
}
