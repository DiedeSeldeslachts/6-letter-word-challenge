using _6LetterWords.WordSegmentProcessing;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6LetterWords.Tests
{
    public class WordSegmentProcessorTests
    {

        [Fact]
        public void WordSegmentProcessor_ReturnsMatches_WhenGivenValidData()
        {
            var input = new FakeWordSegmentResult(
                categorizedWords: new Dictionary<int, List<string>>
                {
                    [1] = new List<string> { "z", "a", "b", "m", "i", "a", "f", "g" },
                    [2] = new List<string> { "za", "fl", "ig", "ht" },
                    [3] = new List<string> { "bia", "amb", "fli" },
                    [4] = new List<string> { "flig", "igth", "zamb", "ambi" },
                    [5] = new List<string> { "zambi", "light" },
                    [6] = new List<string> { "zambia", "flight" }
                },
                splitUpSixLetterWords: new Dictionary<string, IReadOnlyDictionary<int, string>>()
                {
                    ["zambia"] = new Dictionary<int, string>()
                    {
                        [0] = "zambia",
                        [1] = "ambia",
                        [2] = "mbia",
                        [3] = "bia",
                        [4] = "ia",
                        [5] = "a"
                    },
                    ["flight"] = new Dictionary<int, string>()
                    {
                        [0] = "flight",
                        [1] = "light",
                        [2] = "ight",
                        [3] = "ght",
                        [4] = "ht",
                        [5] = "t"
                    }
                });

            var processor = new WordSegmentProcessor(6, 1);


            var combinations = processor.FindWordCombinationsFromSegments(input);


            combinations.WordMatches.Count().Should().Be(13);
            combinations.WordMatches.Where(x => x.Match == "zambia").Count().Should().Be(8);
            combinations.WordMatches.Where(x => x.Match == "flight").Count().Should().Be(5);
            combinations.WordMatches.Where(x => String.Join("", x.Segments) == "flight" || String.Join("", x.Segments) == "zambia").Count().Should().Be(13);
        }
    }
}
