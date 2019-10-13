using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperNetCore.Model
{
    public class Board
    {
        int[,] _boardArray = new int[9, 9];

        public int this[int indexRow, int indexColumn]
        {
            get { return _boardArray[indexRow, indexColumn]; }
            set { _boardArray[indexRow, indexColumn] = value; }
        }
    }
}
