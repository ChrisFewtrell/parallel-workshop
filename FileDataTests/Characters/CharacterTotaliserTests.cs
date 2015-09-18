using FileData.Characters;
using NUnit.Framework;

namespace FileDataTests.Characters
{
    [TestFixture]
    public class CharacterTotaliserTests
    {
        [Test]
        public void Add_ShouldGiveCharCountsOfCharacters()
        {
            var totaliser = new CharacterTotaliser();
            totaliser.Add("abca.");
            totaliser.Add("foo bar wibble!");
            var counts = totaliser.GetCharCounts();
            Assert.That(counts['a'], Is.EqualTo(3));
            Assert.That(counts['b'], Is.EqualTo(4));
        }
    }
}
