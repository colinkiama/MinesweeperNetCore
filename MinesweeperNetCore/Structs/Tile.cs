using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperNetCore.Structs
{
    public struct Tile
    {
        public bool IsVisible;
        public int Value;

        public override string ToString()
        {
            string returnString;
            if (IsVisible)
            {
                returnString = GetStringIfVisible();
                
            }
            else
            {
                returnString = "X";
            }

            return returnString;
        }

        private string GetStringIfVisible()
        {
            string stringToReturn;
            if (Value != Game.MineValue)
            {
                stringToReturn = $"{Value}";
            }
            else
            {
                stringToReturn = "*";
            }

            return stringToReturn;
        }
    }
}
