using _6LetterWords.WordSegmentParsing;
using _6LetterWords.WordSegmentProcessing;
using System.Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        string executableLocation = Path.GetDirectoryName(path: Assembly.GetExecutingAssembly().Location);
        string inputLocation = Path.Combine(executableLocation, "input.txt");

        var input = new FileWordSegementParser(6, 1).Parse(inputLocation);
        var output = new WordSegmentProcessor(6, 1).FindWordCombinationsFromSegments(input);

        Console.WriteLine(output);
        Console.ReadLine();
    }
}