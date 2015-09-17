namespace FileData.Reading
{
    public class EmbeddedTextFileSource : ITextFileSource
    {
        public ITextFile GetTextFile(string fileName)
        {
            // Check that it exists here, avoiding unexpected exceptions later
            return FileData.HasResource(fileName) ? new EmbeddedTextFile(fileName) : null;
        }
    }
}
