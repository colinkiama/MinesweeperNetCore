using MinesweeperNetCore.Enums;
using System;

namespace MinesweeperNetCore
{
    class Program
    {
        private static bool hasUserQuitProgram = false;
        public static GameState CurrentState = GameState.None;
        static void Main(string[] args)
        {
            DisplayMainMenuOptions();
            while (!hasUserQuitProgram)
            {
                switch (CurrentState)
                {
                    case GameState.MainMenu:
                        ReadUserOptionInput();
                        break;
                    case GameState.None:
                        DisplayMainMenuOptions();
                        break;
                    default:
                        break;
                }
                
            }
        }

        private static void ReadUserOptionInput()
        {
            string optionInput = Console.ReadLine();
            var treatedInput = optionInput.Trim();
            char parsedOption;
            bool isValidChar = char.TryParse(treatedInput, out parsedOption);
            if (!isValidChar || !CheckIfValidOption(parsedOption))
            {
                DisplayInvalidOptionMessage();
            }
        }

        private static void DisplayInvalidOptionMessage()
        {
            Console.WriteLine("You did not enter a valid option, please try again");
            CurrentState = GameState.None;
        }

        private static bool CheckIfValidOption(char parsedOption)
        {
            return false;
        }

        private static void DisplayMainMenuOptions()
        {
            PrintOption('1', "Start new game");
            PrintOption('q', "Quit");
            CurrentState = GameState.MainMenu;
        }

        private static void PrintOption(char optionKey, string optionDescription)
        {
            Console.WriteLine($"[{Char.ToUpper(optionKey)}] {optionDescription}");
        }
    }
}
