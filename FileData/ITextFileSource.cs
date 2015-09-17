namespace FileData
{
    public interface ITextFileSource
    {
        /// <summary>
        /// Get the named file.
        /// </summary>
        /// <param name="fileName">The file name (no path or namespace).</param>
        /// <returns>The file, or null if not found.</returns>
        ITextFile GetTextFile(string fileName);
    }
}
