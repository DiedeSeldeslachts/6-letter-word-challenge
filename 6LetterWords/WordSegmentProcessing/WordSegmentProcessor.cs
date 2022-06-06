using _6LetterWords.WordSegmentParsing;
using System.Diagnostics.CodeAnalysis;

namespace _6LetterWords.WordSegmentProcessing
{
    public class WordSegmentProcessor : IWordSegmentProcessor
    {
        public int MaxWordSize { get; }

        public int MinSegmentSize { get; }

        public WordSegmentProcessor(int maxWordSize, int minSegmentSize)
        {
            MaxWordSize = maxWordSize;
            MinSegmentSize = minSegmentSize;
        }

        /// <summary>
        /// Find word combinations given segements of words
        /// </summary>
        /// <param name="input">An object holding all seperate segments of words as well as the words to search for</param>
        /// <returns>All possible combinations</returns>
        public WordCombinations FindWordCombinationsFromSegments(IWordSegmentResult input)
        {
            var result = new List<WordMatch>();
            //Start from the biggest words
            foreach (var splitUpSixLetterWord in input.SegmentedMaxLengthWords)
            {
                var segementsList = FindNextSegments(splitUpSixLetterWord.Key, 0, input);
                foreach (var segements in segementsList)
                {
                    result.Add(new WordMatch(segements, splitUpSixLetterWord.Key));
                }
            }
            return new WordCombinations(result);
        }

        /// <summary>
        /// Recursively searches for new segemetns that fit the word we are currently searching
        /// </summary>
        /// <param name="wordToSearch">Word to seach segements for</param>
        /// <param name="baseStr">The current part of the string we already managed to find</param>
        /// <param name="input">All segments and words we can search for</param>
        /// <param name="currentList">The current segments </param>
        /// <param name="segments"></param>
        /// <returns></returns>
        private List<List<string>> FindNextSegments([NotNull] string wordToSearch, [NotNull] int currentSegmentsLength, IWordSegmentResult input, List<string> segments = null, List<List<string>> segmentsList = null)
        {
            if (segmentsList == null) segmentsList = new List<List<string>>();
            if (segments == null) segments = new List<string>();

            for (int i = 1; i <= MaxWordSize - currentSegmentsLength; i++)
            {
                //Don't check for words of the maximum lenght, then it is not a combination
                if (i > MaxWordSize - 1) break;
                //The readability of this should be improved
                var firstLetterMatches = input.CategorizedWords[i].FirstOrDefault(x => input.SegmentedMaxLengthWords[wordToSearch][currentSegmentsLength][0] == x[0] //This is a performance optimalization (just check first char in string), (10 times faster than just using startsWith)
                                                                                       && input.SegmentedMaxLengthWords[wordToSearch][currentSegmentsLength].StartsWith(x)); //This is 90% of all processing time, can possibly be improved 
                if (firstLetterMatches != null)
                {
                    var newList = new List<string>(segments);
                    newList.Add(firstLetterMatches);

                    if (currentSegmentsLength + firstLetterMatches.Length == MaxWordSize)
                    {
                        segmentsList.Add(newList);
                    }
                    else
                    {
                        segmentsList = FindNextSegments(wordToSearch, currentSegmentsLength + firstLetterMatches.Length, input, newList, segmentsList);
                    }
                }
            }

            return segmentsList;
        }
    }
}
