using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace wordle_peaks_solver
{
    internal class DictionaryCheck
    {
        private const string LEGAL_WORDS_PATH = @"..\..\..\legalEntries.txt";

        private readonly IList<string> _words;
        private readonly HashSet<string> _legalEntries;

        public DictionaryCheck(IEnumerable<string> words)
        {
            _words = words.ToList();

            var rawLegal = File.ReadLines(LEGAL_WORDS_PATH).First().Replace("\"", "");
            _legalEntries = new HashSet<string>(rawLegal.Split(','));
        }

        public void IllegalWordCheck()
        {
            var results = new List<string>();

            foreach (var word in _words)
            {
                if (!_legalEntries.Contains(word))
                    results.Add(word);
            }

            if (results.Count == 0)
                Console.WriteLine("All entries in dictionary are legal!");
            else
            {
                Console.WriteLine("The following dictionary entries are invalid:");
                foreach (var word in results)
                    Console.WriteLine(word);
            }
        }
    }
}