namespace _6LetterWords.WordSegmentProcessing
{
    public class WordCombinations
    {
        public IEnumerable<WordMatch> WordMatches { get; }

        public WordCombinations(IEnumerable<WordMatch> sixLetterWordMatches)
        {
            WordMatches = sixLetterWordMatches;
        }

        public override string ToString()
        {
            return WordMatches.Select(x => x.ToString()).Aggregate((m1, m2) => $"{m1}\r\n{m2}");
        }
    }
}
