using _6LetterWords.WordSegmentParsing;

namespace _6LetterWords.WordSegmentProcessing
{
    public interface IWordSegmentProcessor
    {
        public int MaxWordSize { get; }
        public int MinSegmentSize { get; }
        public WordCombinations FindWordCombinationsFromSegments(IWordSegmentResult input);
    }
}
