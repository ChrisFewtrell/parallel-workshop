using FileData.Characters;

namespace ParallelWorkshop.Ex03ProducerConsumer
{
    public class MultiFileCharacterCounter : ICharacterCounter
    {
        private readonly CharacterTotaliser totaliser = new CharacterTotaliser();

        public System.Collections.Generic.IReadOnlyDictionary<char, int> CharCounts
        {
            get { return totaliser.CharCounts; }
        }
    }
}
