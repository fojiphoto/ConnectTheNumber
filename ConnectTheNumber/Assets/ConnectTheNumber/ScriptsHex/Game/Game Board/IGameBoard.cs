using System.Collections.Generic;

namespace Ilumisoft.Hex
{
    public interface IGameBoard : IGameGrid, IGameTileFactory
    {
        IList<GameTile> GameTiles { get; }
    }
}