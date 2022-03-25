using System;
using System.Collections.Generic;
using System.Linq;

namespace wordle_peaks_solver
{
    public class TestGame
    {
        private const int GUESS_COUNT = 6;
        private const int TEST_SIZE = 4000;
        private readonly bool SHOW_WORD_DETAILS = false;

        private readonly IList<string> _words;

        public TestGame(IEnumerable<string> words)
        {
            _words = words.ToList();
        }

        public void RunTest()
        {
            int start = Environment.TickCount;
            Console.WriteLine($"Running test: {TEST_SIZE} words");
            var testWords = _words.Take(TEST_SIZE);
            var results = new ResultDistribution(GUESS_COUNT);
            foreach (var word in testWords)
            {
                var score = PlayGame(word);
                if (score.HasValue)
                    results.ScoreCount[score.Value]++;
                else
                {
                    results.Misses++;
                    if (SHOW_WORD_DETAILS)
                        Console.WriteLine($"Missed: {word}");
                }
            }
            Console.Write(results);
            Console.WriteLine($"Time: {Environment.TickCount - start} ms");
        }

        public int? PlayGame(string target)
        {
            var options = new PossibleWords(_words);

            for (int i = 0; i < 6; i++)
            {
                var currGuess = options.BestGuess();
                var result = GetResult(currGuess, target);
                if (result == "GGGGG")
                {
                    return i + 1;
                }
                options.AddClue(currGuess, result);
            }
            return null;
        }

        private string GetResult(string guess, string target)
        {
            string result = "";
            for (var i = 0; i < 5; i++)
            {
                if (guess[i] == target[i])
                    result += "G";
                else if (guess[i] < target[i])
                    result += "O";
                else
                    result += "B";
            }
            return result;
        }
    }
}