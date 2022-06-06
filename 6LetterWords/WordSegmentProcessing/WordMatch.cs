namespace _6LetterWords.WordSegmentProcessing
{
    public class WordMatch
    {
        public List<string> Segments { get; }
        public string Match { get; }

        public WordMatch(List<string> segments, string match)
        {
            Segments = segments;
            Match = match;
        }

        public override string ToString()
        {
            return $"{Segments.Aggregate((w1, w2) => $"{w1}+{w2}")}={Match}";
        }
    }
}
