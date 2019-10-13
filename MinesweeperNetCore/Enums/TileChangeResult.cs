using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperNetCore.Enums
{
    public enum TileChangeResult
    {
        AlreadyRevealed,
        Mine,
        Revealed,
        UnFlagged,
        Flagged,
        FlagUnavailable
    }
}
