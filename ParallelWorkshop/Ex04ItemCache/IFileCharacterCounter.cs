using Lurchsoft.FileData;
using Lurchsoft.FileData.Characters;

namespace Lurchsoft.ParallelWorkshop.Ex04ItemCache
{
    public interface IFileCharacterCounter
    {
        ICharacterCounter GetCharCounts(ITextFile textFile);
    }
}
