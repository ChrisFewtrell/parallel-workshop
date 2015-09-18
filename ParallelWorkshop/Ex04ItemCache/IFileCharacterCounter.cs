using FileData;
using FileData.Characters;

namespace ParallelWorkshop.Ex04ItemCache
{
    public interface IFileCharacterCounter
    {
        ICharacterCounter GetCharCounts(ITextFile textFile);
    }
}
