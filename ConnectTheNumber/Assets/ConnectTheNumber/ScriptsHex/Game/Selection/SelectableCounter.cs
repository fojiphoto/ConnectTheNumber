using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.Hex
{
    public class SelectableCounter
    {
        IGameGrid grid;
        IConnectionValidator connectionValidator;

        Vector2[] directions;

        Selection selection;

        public SelectableCounter(IGameGrid grid)
        {
            this.selection = new Selection();
            this.selection.Clear();

            this.grid = grid;

            this.connectionValidator = new LevelValidator(selection);

            directions = new Vector2[]
            {
                Vector2.up,
                Vector2.right + Vector2.up/2,
                Vector2.right + Vector2.down/2,
                Vector2.down,
                Vector2.left+ Vector2.up/2,
                Vector2.left + Vector2.down/2,
            };
        }

        /// <summary>
        /// Returns the number of all GameTiles which are selectable from the given one
        /// </summary>
        /// <param name="gameTile"></param>
        /// <returns></returns>
        public int Count(GameTile gameTile)
        {
            selection.Clear();

            if (gameTile != null && gameTile.IsDestroyed == false)
            {
                AddToSelection(gameTile);
            }

            return selection.Count;
        }

        public void AddToSelection(GameTile gameTile)
        {
            if (selection.Contains(gameTile))
            {
                return;
            }

            selection.Add(gameTile);

            gameTile.DisableCollider();

            foreach (var direction in directions)
            {
                TrySelect(gameTile, direction);
            }

            gameTile.EnableCollider();
        }

        public void TrySelect(GameTile gameTile, Vector2 direction)
        {
            float maxDistance = Mathf.Sqrt(2 * grid.CellSize * grid.CellSize);

            var raycast = new GameTileRaycast(gameTile.transform.position, direction, maxDistance);

            if (raycast.Perform(out var neighbor))
            {
                if (connectionValidator.IsValid(gameTile, neighbor))
                {
                    AddToSelection(neighbor);
                }
            }
        }
    }
}