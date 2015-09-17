using FileData;
using NUnit.Framework;

namespace FileDataTests
{
    [TestFixture]
    public class EmbeddedFilesTests
    {
        [Test]
        public void Medium_ShouldNotBeNull()
        {
            Assert.That(EmbeddedFiles.Medium, Is.Not.Null);
        }

        [Test]
        public void Large_ShouldNotBeNull()
        {
            Assert.That(EmbeddedFiles.Large, Is.Not.Null);
        }

        [Test]
        public void Huge_ShouldNotBeNull()
        {
            Assert.That(EmbeddedFiles.Huge, Is.Not.Null);
        }
    }
}
