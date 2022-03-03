using System.IO;

namespace wordle_peaks_solver
{
    internal class Program
    {
        private const string INTERACTIVE_ARG = "-p";
        private const string TEST_ARG = "-t";

        private const string FILE_PATH = @"..\..\..\words.txt";

        private static void Main(string[] args)
        {
            var words = File.ReadLines(FILE_PATH);

            var arg = args.Length > 0 ? args[0] : INTERACTIVE_ARG;

            switch (arg)
            {
                case TEST_ARG:
                    new TestGame(words).RunTest();
                    break;

                case INTERACTIVE_ARG:
                default:
                    new InteractiveGame(words).PlayGame();
                    break;
            }
        }
    }
}