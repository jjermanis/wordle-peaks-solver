using System;
using System.Collections.Generic;
using System.Linq;

namespace wordle_peaks_solver
{
    internal class PossibleWords
    {
        private static readonly HashSet<char> VALID_RESULT_CHARS = new HashSet<char> { 'G', 'B', 'O' };

        private readonly char[] _low;
        private readonly char[] _high;
        private List<string> _options;
        private OptimalGuess _optimalGuess;

        public PossibleWords(IEnumerable<string> words)
        {
            _low = new char[5];
            _high = new char[5];
            for (var x = 0; x < 5; x++)
            {
                _low[x] = 'a';
                _high[x] = 'z';
            }

            _options = words.ToList();
            UpdateOptions();
        }

        public string BestGuess()
            => _options.FirstOrDefault();

        public int Score(string word)
        {
            var result = 1;
            var opt = _optimalGuess.Word;

            for (var i = 0; i < 5; i++)
            {
                var score = 26 - (Math.Abs(word[i] - opt[i]));
                result *= score;
            }

            return result;
        }

        public void AddClue(string guess, string result)
        {
            for (int i = 0; i < 5; i++)
            {
                switch (result[i])
                {
                    case 'G':
                        _low[i] = guess[i];
                        _high[i] = guess[i];
                        break;

                    case 'O':
                        _low[i] = (char)(guess[i] + 1);
                        break;

                    case 'B':
                        _high[i] = (char)(guess[i] - 1);
                        break;
                }
            }

            var newList = new List<string>();
            foreach (var word in _options)
            {
                var isMatch = true;
                for (var i = 0; i < 5; i++)
                {
                    if (word[i] < _low[i] || word[i] > _high[i])
                        isMatch = false;
                }
                if (isMatch)
                    newList.Add(word);
            }
            _options = newList;
            UpdateOptions();
        }

        public static bool IsValidResult(string result)
        {
            if (result.Length != 5)
                return false;
            foreach (var c in result)
                if (!VALID_RESULT_CHARS.Contains(c))
                    return false;
            return true;
        }

        private void UpdateOptions()
        {
            _optimalGuess = new OptimalGuess(_options);
            _options = _options.OrderByDescending(w => Score(w)).ToList();
        }
    }
}