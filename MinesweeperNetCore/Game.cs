using MinesweeperNetCore.Model;
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
            Console.WriteLine("Enter position");
            string input = Console.ReadLine();
        }
    }
}
