using System.IO;
using System.Reflection;

namespace FileData
{
    internal static class FileData
    {
        public static Stream OpenResource(string fileName)
        {
            try
            {
                return Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(FileData), fileName);
            }
            catch (IOException)
            {
                return null;
            }
        }
    }
}
