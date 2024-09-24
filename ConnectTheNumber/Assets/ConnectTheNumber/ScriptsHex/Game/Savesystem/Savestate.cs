using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.Hex
{
    [System.Serializable]
    public class Savestate
    {
        [System.Serializable]
        public struct TileData
        {
            public int Value;
            public Vector3 Position;
        }

        [SerializeField]
        int maxReachedLevel = 0;

        [SerializeField]
        int score = 0;

        [SerializeField]
        List<TileData> tiles = new List<TileData>();

        public int MaxReachedLevel { get => this.maxReachedLevel; set => this.maxReachedLevel = value; }

        public int Score { get => this.score; set => this.score = value; }

        public List<TileData> Tiles { get => this.tiles; set => this.tiles = value; }
    }
}