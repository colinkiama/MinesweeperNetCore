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
            int inputRow = InputLoop("Enter row number (Vertical):");
            int inputColumn = InputLoop("Enter Column Number (Horizontal: ");
            // Takeaway 1 from inputs because the board array is zero-indexed
            HandlePositionInput(inputRow - 1, inputColumn -1);

        }

        private void HandlePositionInput(int rowNumber, int columnNumber)
        {
            TileRevealResult result = gameBoard.RevealTile(rowNumber, columnNumber);
        }

        private int InputLoop(string inputMessage)
        {
            int validInput = int.MaxValue;
            bool wasCorrectInput = false;

            while (!wasCorrectInput)
            {
                Console.WriteLine(inputMessage);
                string input = Console.ReadLine();
                InputParseResult parseResult = CoordinateInputHelper.ParseInput(input);
                if (parseResult.ErrorResult != Enums.InputParseError.None)
                {
                    DisplayErrorMessage(parseResult.ErrorResult, input);

                }
                else
                {
                    validInput = parseResult.ParsedInput;
                    wasCorrectInput = true;
                }
            }

            return validInput;
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
