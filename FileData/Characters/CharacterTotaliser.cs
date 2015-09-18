using System.Collections.Generic;
namespace FileData.Characters
{
    public class CharacterTotaliser : ITotaliser, ICharacterCounter
    {
        private readonly Dictionary<char, int> charCounts = new Dictionary<char,int>();

        public void Add(string text)
        {
            foreach (char c in text)
            {
                int curCount;
                charCounts.TryGetValue(c, out curCount); // charCount is 0 if not found!
                charCounts[c] = curCount + 1;
            }
        }

        public IReadOnlyDictionary<char, int> GetCharCounts()
        {
            // Clone, so that results are captured at the time of calling this property
            return new Dictionary<char, int>(charCounts);
        }
    }
}
