using System.Collections.Generic;

namespace FileData
{
    public interface ITextFile
    {
        /// <summary>
        /// The lines of the text file. Note that retrieving each line might, or might not, block.
        /// </summary>
        IEnumerable<string> ReadLines();
    }
}
