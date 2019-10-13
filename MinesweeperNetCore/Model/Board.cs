using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperNetCore.Model
{
    public class Board
    {
        Tile[,] _boardArray = new Tile[9, 9];
        int numberOfMines = 0;
        Random rnd = new Random();

        public int Width => _boardArray.Length;
        public Tile this[int indexRow, int indexColumn]
        {
            get { return _boardArray[indexRow, indexColumn]; }
            set { _boardArray[indexRow, indexColumn] = value; }
        }


        public void DisplayBoard()
        {
            for (int row = 0; row < this.Width; row++)
            {
                for (int column = 0; column < this.Width; column++)
                {

                }
            }
        }

        internal void FillBoard()
        {
            GenerateMines();
            IdentifyNeighbourTiles();
        }

        private void IdentifyNeighbourTiles()
        {
            for (int row = 0; row < this.Width; row++)
            {
                for (int column = 0; column < this.Width; column++)
                {

                    this[row, column] = new Tile
                    {
                        Value = CheckForNearbyMines(row, column),
                        IsVisible = false
                    };
                }
            }
        }

        private int CheckForNearbyMines(int row, int column)
        {
            int nearbyMines = 0;
            nearbyMines += CheckForHorizontalMines(row, column);
            nearbyMines += CheckForVerticalMines(row, column);
            nearbyMines += CheckForDiagonalMines(row, column);
            return nearbyMines;
        }

        private int CheckForDiagonalMines(int row, int column)
        {
            int mines = 0;
            int upRow = row - 1;
            int downRow = row + 1;
            int leftColumn = column - 1;
            int rightColumn = column + 1;


            // Up-Left Diagonal
            if (upRow > -1 && leftColumn > -1)
            {
                if (this[upRow, leftColumn].Value == Game.MineValue)
                {
                    mines += 1;
                }
            }

            // Up-Right Diagonal
            if (upRow > -1 && rightColumn < this.Width)
            {
                if (this[upRow, rightColumn].Value == Game.MineValue)
                {
                    mines += 1;
                }
            }

            // Down-Left Diagonal
            if (downRow < this.Width && leftColumn > -1)
            {
                if (this[downRow, leftColumn].Value == Game.MineValue)
                {
                    mines += 1;
                }
            }


            // Down-Right Diagonal
            if (downRow < this.Width && rightColumn < this.Width)
            {
                if (this[downRow, rightColumn].Value == Game.MineValue)
                {
                    mines += 1;
                }
            }
            return mines;
        }

        private int CheckForVerticalMines(int row, int column)
        {
            int mines = 0;

            // Up
            int upRow = row - 1;

            if (upRow > -1)
            {
                if (this[upRow, column].Value == Game.MineValue)
                {
                    mines += 1;
                }
            }

            // Down
            int downRow = row + 1;
            if (downRow < this.Width)
            {
                if (this[downRow, column].Value == Game.MineValue)
                {
                    mines += 1;
                }
            }
            return mines;
        }

        private int CheckForHorizontalMines(int row, int column)
        {
            int mines = 0;

            // Left
            int leftColumn = column - 1;
            if (leftColumn > -1)
            {
                if (this[row, leftColumn].Value == Game.MineValue)
                {
                    mines += 1;
                }
            }


            // Right
            int rightColumn = column + 1;
            if (rightColumn < this.Width)
            {
                if (this[row, rightColumn].Value == Game.MineValue)
                {
                    mines += 1;
                }
            }


            return mines;
        }

        private void GenerateMines()
        {
            for (int row = 0; row < _boardArray.Length; row++)
            {
                for (int column = 0; column < _boardArray.Length; column++)
                {
                    int tileValue = GenerateMineTileValue();
                    if (tileValue == Game.MineValue)
                    {
                        _boardArray[row, column] = new Tile
                        {
                            Value = tileValue,
                            IsVisible = false
                        };

                        numberOfMines += 1;
                    }
                }
            }
        }

        private int GenerateMineTileValue()
        {
            // Random number from -1 to random number from 6 to 10
            // so there is a 8.33%-12.5% chance of the tile being a mine
            return rnd.Next(-1, rnd.Next(6, 12));
        }
    }
}
