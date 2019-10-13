using System;

namespace MinesweeperNetCore
{
    class Program
    {
        public static bool hasUserQuitProgram = false;
        static void Main(string[] args)
        {
            DisplayMainMenuOptions();
            while (!hasUserQuitProgram)
            {
            }
        }

        private static void DisplayMainMenuOptions()
        {
            int optionNumber = 1;
            PrintOption(ref optionNumber, "This");
            PrintOption(ref optionNumber, "is");
            PrintOption(ref optionNumber, "a");
            PrintOption(ref optionNumber, "Test");
        }

        private static void PrintOption(ref int optionNumber, string optionDescription)
        {
            Console.WriteLine($"[{optionNumber}] {optionDescription}");
            optionNumber++;
        }
    }
}
