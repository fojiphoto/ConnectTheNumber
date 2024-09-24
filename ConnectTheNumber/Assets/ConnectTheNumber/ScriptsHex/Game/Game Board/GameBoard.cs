using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.Hex
{
    public class GameBoard : GameGrid, IGameBoard
    {
        public static int MaxReachedLevel = 3;

        [SerializeField]
        AbstractGameTileFactory gameTileFactory = null;

        public IList<GameTile> GameTiles => GameTileManager.Instance.GameTiles;

        public GameTile Spawn(GameTile prefab, Vector3 position)
        {
            var gameTile = gameTileFactory.Spawn(position);
            gameTile.transform.localScale *= CellSize;
            
            return gameTile;
        }

        public GameTile Spawn(Vector3 position)
        {
            var gameTile = gameTileFactory.Spawn(position);
            gameTile.transform.localScale *= CellSize;
            
            return gameTile;
        }

        public void Spawn()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var position = GetPosition(x, y);
                    Spawn(position);
                }
            }
        }

       
    }
}