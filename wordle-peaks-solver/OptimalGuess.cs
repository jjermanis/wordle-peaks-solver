using System.Collections.Generic;

namespace wordle_peaks_solver
{
    internal class OptimalGuess
    {
        internal readonly string _word;

        public OptimalGuess(IList<string> words)
        {
            // Calcualte distribution of letters in each position
            var freq = new int[5, 26];
            foreach (var word in words)
                for (var x = 0; x < 5; x++)
                    freq[x, word[x] - 'a']++;

            // Find out which letter is at the midpoint at each position
            var result = new char[5];
            var midCount = (words.Count + 1) / 2;
            for (var x = 0; x < 5; x++)
            {
                var sum = 0;
                for (var c = 'a'; c <= 'z'; c++)
                {
                    sum += freq[x, c - 'a'];
                    if (sum >= midCount)
                    {
                        result[x] = c;
                        break;
                    }
                }
            }
            _word = new string(result);
        }

        public string Word { get => _word; }
    }
}