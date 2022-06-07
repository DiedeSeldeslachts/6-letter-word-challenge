using _6LetterWords.WordSegmentParsing;
using FluentAssertions;
using System.Reflection;

namespace _6LetterWords.Tests
{
    public class WordSegmentParsingTests
    {
        [Theory]
        [InlineData("input.txt", "s", "h", "zambia", "basalt", 6, 1, 764, 382)]
        [InlineData("input2.txt", "s", "h", "zambia", "flight", 6, 1, 98, 65)]
        public void FileWordSegmentParser_ReturnsWordSegments_WhenGivenValidFilePath(string fileName, string minLetterFirst, string minLetterLast, string maxLetterFirst, string maxLetterLast, int maxWordSize, int minSegmentSize, int minLengthArraySize, int maxLengthArraySize)
        {
            var fileParser = new FileWordSegementParser(maxWordSize, minSegmentSize);
            string executableLocation = Path.GetDirectoryName(path: Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(executableLocation, fileName);

            var fileParseResult = fileParser.Parse(filePath);

            fileParseResult.CategorizedWords.Should().NotBeNullOrEmpty();
            //I know this does not have to be done this way, i could have done simple checks, but i wanted to check out some pattern matching
            var are1LetterWordsCorrectlyPopulated = fileParseResult.CategorizedWords[1] is [var minFirst, .., var minLast] category && minFirst == minLetterFirst && minLast == minLetterLast && category.Count() == minLengthArraySize; 
            var are6LetterWordsCorrectlyPopulated = fileParseResult.CategorizedWords[6] is [var maxFirst, .., var maxLast] category2 && maxFirst == maxLetterFirst && maxLast == maxLetterLast && category2.Count() == maxLengthArraySize;
            are1LetterWordsCorrectlyPopulated.Should().BeTrue();
            are6LetterWordsCorrectlyPopulated.Should().BeTrue();
        }

        [Fact]
        public void FileWordSegmentParser_ThrowsFileNotFoundException_WhenGivenInvalidFilePath()
        {
            var fileParser = new FileWordSegementParser(6, 1);
            string executableLocation = Path.GetDirectoryName(path: Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(executableLocation, "fake-name.txt");

            var act = () => fileParser.Parse(filePath);

            act.Should().Throw<FileNotFoundException>();
        }

        [Fact]
        public void FileWordSegmentParser_ArgumentOutOfRangeException_WhenGivenInvalidFile()
        {
            var fileParser = new FileWordSegementParser(6, 1);
            string executableLocation = Path.GetDirectoryName(path: Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(executableLocation, "faultyInput.txt");

            var act = () => fileParser.Parse(filePath);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}