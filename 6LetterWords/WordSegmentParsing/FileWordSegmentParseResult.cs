using System.Collections.ObjectModel;

namespace _6LetterWords.WordSegmentParsing
{
    public class FileWordSegmentParseResult : IWordSegmentResult
    {

        public FileWordSegmentParseResult(IReadOnlyDictionary<int, List<string>> categorizedWords, IReadOnlyDictionary<string, IReadOnlyDictionary<int, string>> splitUpSixLetterWords)
        {
            CategorizedWords = categorizedWords;
            SplitUpSixLetterWords = splitUpSixLetterWords;
        }
    }
}
