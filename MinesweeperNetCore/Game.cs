using MinesweeperNetCore.Enums;
using MinesweeperNetCore.Helpers;
using MinesweeperNetCore.Model;
using MinesweeperNetCore.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperNetCore
{
    public class Game
    {
        int gameBoardSize = 9;
        public const int MineValue = -1;
        public const int BlankTileValue = 0;

        bool hasGameEnded = false;
        Board gameBoard;
        // Program will create this object then start a game with it. When game ends, object is destroyed 
        // => Context returns to program, which will switch the state back to none 
        internal void Start()
        {
            gameBoard = new Board(gameBoardSize);
            Console.Clear();
            while (!hasGameEnded)
            {
                gameBoard.FillBoard();
                gameBoard.DisplayBoard();
                RequestUserInput();
            }
        }

        private void RequestUserInput()
        {
            bool wasCorrectInput = false;
            while (!wasCorrectInput)
            {
                Console.WriteLine("Enter row number (Vertical): ");
                Console.Write("Enter Column Number (Horizontal: ");
                string input = Console.ReadLine();
                var parseResult = CoordinateInputHelper.ParseInput(input);
                if (parseResult.ErrorResult == Enums.InputParseError.None)
                {
                    wasCorrectInput = HandleParseResult(parseResult);
                }
                else
                {
                    DisplayErrorMessage(parseResult.ErrorResult, input);
                }
            }
        }

        private bool HandleParseResult(InputParseResult parseResult)
        {
            throw new NotImplementedException();
        }

        private void DisplayErrorMessage(InputParseError errorResult, string input)
        {
            switch (errorResult)
            {
                case InputParseError.NumberOutOfRange:
                    Console.WriteLine($"The value you entered: \"{input}\" is outside of the range between" +
                        $"1 and {gameBoardSize}");
                    break;
                case InputParseError.NotANumber:
                    Console.WriteLine($"The value you entered: \"{input}\" is not an Integer number");
                    break;
                default:
                    break;
            }
        }

       
    }
}
