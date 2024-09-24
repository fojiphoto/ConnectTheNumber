using UnityEngine;

namespace Ilumisoft.Hex
{
    public interface IGameTileFactory
    {
        GameTile Spawn(GameTile prefab, Vector3 position);
        GameTile Spawn(Vector3 position);
    }
}