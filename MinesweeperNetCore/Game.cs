using MinesweeperNetCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperNetCore
{
    public class Game
    {
        public const int MineValue = -1;
        public const int BlankTileValue = 0;


        Board gameBoard = new Board();
        // Program will create this object then start a game with it. When game ends, object is destroyed 
        // => Context returns to program, which will switch the state back to none 
        internal void Start()
        {
            Console.Clear();
            gameBoard.FillBoard();
        }
    }
}
