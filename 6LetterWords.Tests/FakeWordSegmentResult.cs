using _6LetterWords.WordSegmentParsing;

namespace _6LetterWords.Tests
{
    internal class FakeWordSegmentResult: IWordSegmentResult
    {
        public FakeWordSegmentResult(IReadOnlyDictionary<int, List<string>> categorizedWords, IReadOnlyDictionary<string, IReadOnlyDictionary<int, string>> splitUpSixLetterWords)
        {
            CategorizedWords = categorizedWords;
            SegmentedMaxLengthWords = splitUpSixLetterWords;
        }
    }
}
