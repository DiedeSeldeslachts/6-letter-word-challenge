using _6LetterWords.WordSegmentParsing;
using _6LetterWords.WordSegmentProcessing;
using System.Diagnostics;
using System.Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        string executableLocation = Path.GetDirectoryName(path: Assembly.GetExecutingAssembly().Location);
        string inputLocation = Path.Combine(executableLocation, "input.txt");
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var input = new FileWordSegementParser(maxWordSize: 6, minSegmentSize: 1).Parse(inputLocation);
        var output = new WordSegmentProcessor(maxWordSize: 6, minSegmentSize: 1).FindWordCombinationsFromSegments(input);
        stopwatch.Stop();
        Console.WriteLine(output);
        Console.WriteLine($"Number of matches: {output.WordMatches.Count()}");
        Console.WriteLine($"Time taken: +- {stopwatch.ElapsedMilliseconds}ms");

        using(FileStream filestream = new FileStream("output.txt", FileMode.Create))
        using (var streamwriter = new StreamWriter(filestream))
        {
            streamwriter.AutoFlush = true;
            streamwriter.Write(output);
        }

        Console.ReadLine();

    }
}