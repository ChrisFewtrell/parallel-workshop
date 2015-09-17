using FileData.Reading;

namespace FileData
{
    public static class EmbeddedFiles
    {
        public const string MediumFileName = "HackersDictionary.txt";
        public const string LargeFileName = "Lovecraft.txt";
        public const string HugeFileName = "LotsOfText.txt";

        private static readonly ITextFileSource Source = new EmbeddedTextFileSource();

        public static readonly ITextFile Medium = Source.GetTextFile(MediumFileName);
        public static readonly ITextFile Large = Source.GetTextFile(LargeFileName);
        public static readonly ITextFile Huge = Source.GetTextFile(HugeFileName);
    }
}
