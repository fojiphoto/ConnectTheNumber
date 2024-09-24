using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.Hex
{
    public class GameTileFactory : AbstractGameTileFactory
    {
        [SerializeField]
        List<GameTile> prefabs = new List<GameTile>();

        GameTileManager GameTileManager => GameTileManager.Instance;

        GameObject gameTileContainer;

        void Awake()
        {
            this.gameTileContainer = new GameObject("Game Tiles");
        }

        public override GameTile Spawn(Vector3 position)
        {
            var prefab = prefabs.GetRandom();

            return Spawn(prefab, position);
        }

        public override GameTile Spawn(GameTile prefab, Vector3 position)
        {
            var gameTile = Instantiate(prefab, position, Quaternion.identity);

            gameTile.transform.SetParent(gameTileContainer.transform);

            GameTileManager.Register(gameTile);

            if (gameTile.TryGetComponent<TileLevelBehaviour>(out var tileLevel))
            {
                tileLevel.SetLevel(Random.Range(0, GameBoard.MaxReachedLevel-1));
            }

            return gameTile;
        }
    }
}