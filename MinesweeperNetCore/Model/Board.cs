﻿using MinesweeperNetCore.Enums;
using MinesweeperNetCore.Helpers;
using MinesweeperNetCore.Structs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MinesweeperNetCore.Model
{
    public class Board
    {
        Tile[,] _boardArray;
        int numberOfMines = 0;
        Random rnd = new Random();

        public int Length { get; private set; }
        public Tile this[int indexRow, int indexColumn]
        {
            get { return _boardArray[indexRow, indexColumn]; }
            set { _boardArray[indexRow, indexColumn] = value; }
        }

        public Board(int boardWidth)
        {
            Length = boardWidth;
            _boardArray = new Tile[9, 9];
        }
        public void DisplayBoard()
        {
            DisplayColumns();
            for (int row = 0; row < this.Length; row++)
            {
                Console.Write($"{row + 1}| ");

                for (int column = 0; column < this.Length; column++)
                {
                    Console.Write($"{this[row, column]} ");
                }
                Console.WriteLine();
            }
        }

        private void DisplayColumns()
        {
            Console.Write("   ");
            for (int i = 0; i < 9; i++)
            {
                Console.Write($"{i + 1}|");
            }
            Console.WriteLine();

            Console.Write("   ");
            for (int i = 0; i < 9; i++)
            {
                Console.Write("= ");
            }
            Console.WriteLine();

        }

        internal TileRevealResult RevealTile(int rowNumber, int columnNumber)
        {
            TileRevealResult result = TileRevealResult.AlreadyRevealed;
            var currentTile = this[rowNumber, columnNumber];
            if (!currentTile.IsVisible)
            {
                currentTile.IsVisible = true;
                if (currentTile.Value != Game.MineValue)
                {
                    result = TileRevealResult.Revealed;
                    if (currentTile.Value == Game.BlankTileValue)
                    {
                        RevealSurroundingTiles(rowNumber, columnNumber);
                    }
                }
                else
                {
                    result = TileRevealResult.Mine;
                }
            }
            return result;
        }

        private void RevealSurroundingTiles(int rowNumber, int columnNumber)
        {
            List<(int row, int column)> surroundingTiles = GetSurroundingTilePositions(rowNumber, columnNumber);
            foreach (var tile in surroundingTiles)
            {
                AttemptToRevealSurroundingTile(tile.row, tile.column);
            }
        }

        private void AttemptToRevealSurroundingTile(int row, int column)
        {
            var currentTile = this[row, column];
            if (currentTile.Value != Game.MineValue)
            {
                currentTile.IsVisible = true;
                if (currentTile.Value == Game.BlankTileValue)
                {
                    RevealSurroundingTiles(row, column);
                }
            }
            
            // You don't reveal the mine
        }

        private List<(int row, int column)> GetSurroundingTilePositions(int row, int column)
        {
            List<(int row, int column)> positionsList = new List<(int row, int column)>();
            
            var result = TileHelper.GetPositionAbove(row, column);
            if (TileHelper.CheckIfPositionIsOutOfBounds(result.column, result.row, PositionOffset.Above) == false)
            {
                positionsList.Add(result);
            }


            result = TileHelper.GetPositionBelow(row, column);
            if (TileHelper.CheckIfPositionIsOutOfBounds(result.column, result.row, PositionOffset.Below) == false)
            {
                positionsList.Add(result);
            }


            result = TileHelper.GetPositionLeftOf(row, column);
            if (TileHelper.CheckIfPositionIsOutOfBounds(result.column, result.row, PositionOffset.Left) == false)
            {
                positionsList.Add(result);
            }

            result = TileHelper.GetPositionRightOf(row, column);
            if (TileHelper.CheckIfPositionIsOutOfBounds(result.column, result.row, PositionOffset.Right) == false)
            {
                positionsList.Add(result);
            }

            result = TileHelper.GetPositionTopLeftOf(row, column);
            if (TileHelper.CheckIfPositionIsOutOfBounds(result.column, result.row, PositionOffset.TopLeft) == false)
            {
                positionsList.Add(result);
            }

            result = TileHelper.GetPositionTopRightOf(row, column);
            if (TileHelper.CheckIfPositionIsOutOfBounds(result.column, result.row, PositionOffset.TopRight) == false)
            {
                positionsList.Add(result);
            }

            result = TileHelper.GetPositionBottomLeftOf(row, column);
            if (TileHelper.CheckIfPositionIsOutOfBounds(result.column, result.row, PositionOffset.BottomLeft) == false)
            {
                positionsList.Add(result);
            }

            result = TileHelper.GetPositionBottomRightOf(row, column);
            if (TileHelper.CheckIfPositionIsOutOfBounds(result.column, result.row, PositionOffset.BottomRight) == false)
            {
                positionsList.Add(result);
            }

            return positionsList;
        }

        internal void FillBoard()
        {
            GenerateMines();
            IdentifyNeighbourTiles();
        }

        private void IdentifyNeighbourTiles()
        {
            for (int row = 0; row < this.Length; row++)
            {
                for (int column = 0; column < this.Length; column++)
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


            
            var result = TileHelper.GetPositionTopLeftOf(row, column);

            if (TileHelper.CheckIfPositionIsOutOfBounds(result.row, result.column, PositionOffset.TopLeft) == false)
            {
                if (this[result.row, result.column].Value == Game.MineValue)
                {
                    mines += 1;
                }
            }

            
            result = TileHelper.GetPositionTopRightOf(row, column);

            if (TileHelper.CheckIfPositionIsOutOfBounds(result.row, result.column, PositionOffset.TopRight) == false)
            {
                if (this[result.row, result.column].Value == Game.MineValue)
                {
                    mines += 1;
                }
            }

            
             result = TileHelper.GetPositionBottomLeftOf(row, column);

            if (TileHelper.CheckIfPositionIsOutOfBounds(result.row, result.column, PositionOffset.BottomLeft) == false)
            {
                if (this[result.row, result.column].Value == Game.MineValue)
                {
                    mines += 1;
                }
            }


            result = TileHelper.GetPositionBottomRightOf(row, column);

            if (TileHelper.CheckIfPositionIsOutOfBounds(result.row, result.column, PositionOffset.BottomRight) == false)
            {
                if (this[result.row, result.column].Value == Game.MineValue)
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
            var result = TileHelper.GetPositionAbove(row, column);

            if (TileHelper.CheckIfPositionIsOutOfBounds(result.row, result.column, PositionOffset.Above) == false)
            {
                if (this[result.row, result.column].Value == Game.MineValue)
                {
                    mines += 1;
                }
            }

            // Down
            result = TileHelper.GetPositionBelow(row, column);

            if (TileHelper.CheckIfPositionIsOutOfBounds(result.row, result.column, PositionOffset.Below) == false)
            {
                if (this[result.row, result.column].Value == Game.MineValue)
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
            var result = TileHelper.GetPositionLeftOf(row, column);

            if (TileHelper.CheckIfPositionIsOutOfBounds(result.row, result.column, PositionOffset.Left) == false)
            {
                if (this[result.row, result.column].Value == Game.MineValue)
                {
                    mines += 1;
                }
            }


            // Right
            result = TileHelper.GetPositionRightOf(row, column);

            if (TileHelper.CheckIfPositionIsOutOfBounds(result.row, result.column, PositionOffset.Right) == false)
            {
                if (this[result.row, result.column].Value == Game.MineValue)
                {
                    mines += 1;
                }
            }


            return mines;
        }

        private void GenerateMines()
        {
            for (int row = 0; row < this.Length; row++)
            {
                for (int column = 0; column < this.Length; column++)
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
