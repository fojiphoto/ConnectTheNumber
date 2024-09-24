using UnityEngine;

namespace Ilumisoft.Hex
{
    public class HexSavesystem : Savesystem
    {
        GameBoard gameBoard;

        private void Awake()
        {
            gameBoard = FindObjectOfType<GameBoard>();
        }

        /// <summary>
        /// Returns true if a savestate exists
        /// </summary>
        /// <returns></returns>
        public override bool HasSavestate()
        {
            return PlayerPrefs.HasKey("Savestate");
        }

        /// <summary>
        /// Saves the current board and score
        /// </summary>
        public override void Save()
        {
            var savestate = new Savestate();

            savestate.Score = Score.Value;
            savestate.MaxReachedLevel = GameBoard.MaxReachedLevel;

            var tiles = gameBoard.GameTiles;

            foreach (var tile in tiles)
            {
                var tileData = new Savestate.TileData()
                {
                    Value = tile.GetComponent<TileLevelBehaviour>().CurrentLevel,
                    Position = tile.transform.position
                };

                savestate.Tiles.Add(tileData);
            }

            var json = JsonUtility.ToJson(savestate);

            PlayerPrefs.SetString("Savestate", json);
        }

        /// <summary>
        /// Loads the board and score from the existing savestate
        /// </summary>
        public override void Load()
        {
            var json = PlayerPrefs.GetString("Savestate");
            var savestate = JsonUtility.FromJson<Savestate>(json);

            GameBoard.MaxReachedLevel = savestate.MaxReachedLevel;
            Score.Value = savestate.Score;
            Score.OnScoreChanged?.Invoke(Score.Value);

            foreach (var tileData in savestate.Tiles)
            {
                var tile = gameBoard.Spawn(tileData.Position);
                tile.GetComponent<TileLevelBehaviour>().CurrentLevel = tileData.Value;
            }
        }

        /// <summary>
        /// Removes any existing savestate
        /// </summary>
        public override void ClearSavestate()
        {
            PlayerPrefs.DeleteKey("Savesate");
        }
    }
}