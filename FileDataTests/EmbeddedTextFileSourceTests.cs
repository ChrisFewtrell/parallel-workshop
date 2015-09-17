using System.Linq;
using FileData;
using FileData.Reading;
using NUnit.Framework;

// ReSharper disable PossibleMultipleEnumeration
namespace FileDataTests
{
    [TestFixture]
    public class EmbeddedTextFileSourceTests
    {
        [Test]
        public void GetTextFile_ShouldReturnNull_WhenFileDoesNotExist()
        {
            Assert.That(new EmbeddedTextFileSource().GetTextFile("does not exist"), Is.Null);
        }

        [Test]
        public void GetTextFile_ShouldReturnDisposable_WhenFileExists()
        {
            Assert.That(new EmbeddedTextFileSource().GetTextFile(EmbeddedFiles.Medium), Is.Not.Null);
        }

        [Test]
        public void GetTextFile_ShouldReturnFileWithSeveralLines()
        {
            var file = new EmbeddedTextFileSource().GetTextFile(EmbeddedFiles.Medium);
            var lines = file.Lines;
            Assert.That(lines.Count(), Is.GreaterThan(10));
        }

        [Test]
        public void GetTextFile_ShouldReturnFileWhoseLinesCanBeEnumeratedMultipleTimes()
        {
            var file = new EmbeddedTextFileSource().GetTextFile(EmbeddedFiles.Medium);
            var lines = file.Lines;
            int count1 = lines.Count();
            int count2 = lines.Count();
            Assert.That(count1, Is.EqualTo(count2));
        }
    }
}
