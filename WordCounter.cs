using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class WordCounter
{
    static Stopwatch stopwatch = new Stopwatch();
    static void Main()
    {
        string filePath = GetValidFilePath();
        var wordCounts = new Dictionary<string, int>();

        stopwatch.Start();
        using (var reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                CountWordsInLine(line, wordCounts);
            }
        }
        stopwatch.Stop();

        PrintWordCounts(wordCounts);
        MeasureTimeAndMemory();
    }
    static void CountWordsInLine(string line, Dictionary<string, int> wordCounts)
    {
        char[] delimiters = { ' ', '\n', '\t'};
        string[] words = line.Split(delimiters)
                            .Select(word => Regex.Replace(word, @"\W+", "").ToLower())
                            .Where(word => !string.IsNullOrWhiteSpace(word))
                            .ToArray();

        foreach (string word in words)
        {
            if (wordCounts.ContainsKey(word))
            {
                wordCounts[word]++;
            }
            else
            {
                wordCounts[word] = 1;
            }
        }
    }

    static void PrintWordCounts(Dictionary<string, int> wordCounts)
    {
        foreach (var wordCount in wordCounts.OrderBy(w => w.Key))
        {
            Console.WriteLine("{0}: {1}", wordCount.Key, wordCount.Value);
        }
    }

    static string GetValidFilePath()
    {
        string filePath = "";
        bool isValidFilePath = false;

        while (!isValidFilePath)
        {
            Console.Write("Enter the path to the input file: ");
            filePath = Console.ReadLine();

            if (File.Exists(filePath))
            {
                isValidFilePath = true;
            }
            else
            {
                Console.WriteLine("Invalid file path. Please enter a valid file path.");
            }
        }

        return filePath;
    }

     static void MeasureTimeAndMemory()
    {
        long memoryUsage = Process.GetCurrentProcess().WorkingSet64;
        Console.WriteLine("Memory usage: {0:N0} MB", memoryUsage / 1000000);

        TimeSpan elapsedTime = stopwatch.Elapsed;
        Console.WriteLine("Execution time: {0:F0} ms", elapsedTime.TotalMilliseconds);
    }

}
