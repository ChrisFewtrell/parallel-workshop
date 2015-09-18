using FileData;
using FileData.Characters;

namespace ParallelWorkshop.Ex03Cache
{
    public interface IFileCharacterCounter
    {
        ICharacterCounter GetCharCounts(ITextFile textFile);
    }
}
