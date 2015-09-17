using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FileData.Reading
{
    internal static class EmbeddedResource
    {
        private static readonly Lazy<ISet<string>> ResourceNames = new Lazy<ISet<string>>(GetResourceNames);

        public static Stream Open(string fileName)
        {
            try
            {
                string resourceName = GetResourceName(fileName);
                return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            }
            catch (IOException)
            {
                return null;
            }
        }

        public static bool Exists(string fileName)
        {
            string resourceName = GetResourceName(fileName);
            return ResourceNames.Value.Contains(resourceName);
        }

        private static ISet<string> GetResourceNames()
        {
            return new HashSet<string>(Assembly.GetExecutingAssembly().GetManifestResourceNames());
        }

        private static string GetResourceName(string fileName)
        {
            return string.Format("FileData.TextFiles.{0}", fileName);
        }
    }
}
