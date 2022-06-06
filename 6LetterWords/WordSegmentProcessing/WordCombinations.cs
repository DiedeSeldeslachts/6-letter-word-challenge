namespace _6LetterWords.WordSegmentProcessing
{
    public class WordCombinations
    {
        public IEnumerable<WordMatch> wordMatches { get; }

        public WordCombinations(IEnumerable<WordMatch> sixLetterWordMatches)
        {
            wordMatches = sixLetterWordMatches;
        }

        public override string ToString()
        {
            return wordMatches.Select(x => x.ToString()).Aggregate((m1, m2) => $"{m1}\r\n{m2}");
        }
    }
}
