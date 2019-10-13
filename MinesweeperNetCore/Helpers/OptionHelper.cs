using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperNetCore.Helpers
{
    public class OptionHelper
    {
        public static void PrintOption(char optionKey, string optionDescription)
        {
            Console.WriteLine($"[{Char.ToUpper(optionKey)}] {optionDescription}");
        }
    }
}
