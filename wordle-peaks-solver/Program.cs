using System;
using System.IO;

namespace wordle_peaks_solver
{
    internal class Program
    {
        private const string FILE_PATH = @"..\..\..\words.txt";

        private static void Main(string[] _)
        {
            var words = File.ReadLines(FILE_PATH);
            var options = new PossibleWords(words);
            Console.WriteLine("Welcome to wordle-peaks-solver. This program will help you solve Wordle Peaks puzzles.");
            Console.WriteLine("To use, guess what this program suggests. Then, let this program know the result.");
            Console.WriteLine("Enter the five colors from the result. G for green, B for blue, and O for orange.");

            for (int i = 0; i < 6; i++)
            {
                var currGuess = options.BestGuess();
                Console.WriteLine($"Please guess: {currGuess}");
                var result = PromptResult();
                if (result == "GGGGG")
                {
                    Console.WriteLine("Congrats!");
                    return;
                }
                options.AddClue(currGuess, result);
            }
            Console.WriteLine("Looks like you ran out of guesses. My fault.");
        }

        private static string PromptResult()
        {
            while (true)
            {
                Console.Write("Result? ");
                var result = Console.ReadLine().ToUpper();
                if (!PossibleWords.IsValidResult(result))
                    Console.WriteLine("Incorrect format. Results should be five letters long. G for green, B for blue, and O for orange.");
                else
                    return result;
            }
        }
    }
}