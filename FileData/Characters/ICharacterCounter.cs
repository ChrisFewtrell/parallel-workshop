using System.Collections.Generic;

namespace Lurchsoft.FileData.Characters
{
    public interface ICharacterCounter
    {
        IReadOnlyDictionary<char, int> GetCharCounts();
    }
}
