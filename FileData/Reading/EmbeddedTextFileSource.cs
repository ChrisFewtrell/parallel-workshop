using System.IO;

namespace FileData.Reading
{
    public class EmbeddedTextFileSource : ITextFileSource
    {
        public ITextFile GetTextFile(string fileName)
        {
            Stream stream = FileData.OpenResource(fileName);
            return stream == null ? null : new EmbeddedTextFile(fileName);
        }
    }
}
