namespace _6LetterWords.WordSegmentParsing
{
    public class FileWordSegementParser : IWordSegmentParser
    {
        public int MaxWordSize { get; }
        public int MinSegmentSize { get; }

        public FileWordSegementParser(int maxWordSize, int minSegmentSize)
        {
            MaxWordSize = maxWordSize;
            MinSegmentSize = minSegmentSize;
        }

        /// <summary>
        /// Parses the contents of a file and categorizes the data
        /// </summary>
        /// <param name="filePath">Path to the file to process</param>
        /// <returns>The categorized data</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public IWordSegmentResult Parse(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Could not find a file on path: {filePath}");
            }

            //var textInMemory = MemoryExtensions.AsMemory(File.ReadAllText(filePath), Index.Start);

            string[] lines = File.ReadAllLines(filePath);

            var categorizedWords = CategegorizeWords(lines);

            return new FileWordSegmentParseResult(categorizedWords, GenerateSegmentsOfWords(categorizedWords[6]));
        }

        /// <summary>
        /// Categorizes words by length
        /// </summary>
        /// <param name="lines">Words to categorize</param>
        /// <returns>Dictionary holding categorized lines</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected IReadOnlyDictionary<int, List<string>> CategegorizeWords(IEnumerable<string> lines)
        {
            var categorizedWords = new Dictionary<int, List<string>>();

            foreach (var size in Enumerable.Range(MinSegmentSize, MaxWordSize))
            {
                categorizedWords.Add(size, new List<string>());
            }

            foreach (var line in lines.Select(x => x.Trim()))
            {
                if (line.Length < MinSegmentSize || line.Length > MaxWordSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(line), $"One of the words has an invalid length of {line.Length}: '{line}'. Valid range is between {MinSegmentSize} and {MaxWordSize}");
                }
                categorizedWords[line.Length].Add(line);
            }

            return categorizedWords;
        }

        /// <summary>
        /// Separates words into segments, e.g. letter => ["letter", "etter", "tter", "ter", "er", "r" ]
        /// This can be used to do checks on certain parts of the words
        /// </summary>
        /// <param name="words">The words to separate into segments</param>
        /// <returns>A dictionary holding the words and their segments</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected IReadOnlyDictionary<string, IReadOnlyDictionary<int, string>> GenerateSegmentsOfWords(IEnumerable<string> words)
        {
            var splitUpWords = new Dictionary<string, IReadOnlyDictionary<int, string>>();

            foreach (var word in words)
            {
                if (word.Length != MaxWordSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(word), $"One of the supposed x-letter words has an invalid length: {word.Length}: '{word}'. Valid range is between {MinSegmentSize} and {MaxWordSize}");
                }

                var splitDictionary = new Dictionary<int, string>();

                for(int i = 0; i < word.Length; i++)
                {
                    var splitString = word.Substring(i, word.Length - i);
                    splitDictionary.Add(i, splitString);
                }
                splitUpWords.TryAdd(word, splitDictionary);
            }

            return splitUpWords;
        }

    }
}
