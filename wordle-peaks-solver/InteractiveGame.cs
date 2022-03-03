using System;
using System.Collections.Generic;

namespace wordle_peaks_solver
{
    internal class InteractiveGame
    {
        private readonly IEnumerable<string> _words;

        public InteractiveGame(IEnumerable<string> words)
        {
            _words = words;
        }

        public void PlayGame()
        {
            var options = new PossibleWords(_words);
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