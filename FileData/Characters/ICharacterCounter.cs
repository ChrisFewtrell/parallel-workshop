using System.Collections.Generic;

namespace FileData.Characters
{
    public interface ICharacterCounter
    {
        IReadOnlyDictionary<char, int> GetCharCounts();
    }
}
