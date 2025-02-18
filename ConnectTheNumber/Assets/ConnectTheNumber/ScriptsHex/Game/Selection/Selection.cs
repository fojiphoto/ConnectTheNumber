﻿using System.Collections.Generic;
using System.Linq;

namespace Ilumisoft.Hex
{
    public class Selection : ISelection
    {
        public List<GameTile> Selected { get; } = new List<GameTile>();

        public bool IsEmpty => Selected.Count == 0;

        public virtual void Clear()
        {
            Selected.Clear();
        }

        public GameTile GetLast()
        {
            if (Count > 0)
            {
                return Selected.Last();
            }
            else
            {
                return null;
            }
        }

        public bool IsLast(GameTile gameTile)
        {
            return Selected.Contains(gameTile) && Selected.Last() == gameTile;
        }

        public virtual void Add(GameTile gameTile)
        {
            if (Contains(gameTile) == false)
            {
                Selected.Add(gameTile);
            }
        }

        public virtual void Remove(GameTile gameTile)
        {
            if (Contains(gameTile))
            {
                Selected.Remove(gameTile);
            }
        }

        public bool Contains(GameTile gameTile)
        {
            return Selected.Contains(gameTile);
        }

        public GameTile Get(int index)
        {
            return Selected[index];
        }

        public int Count => Selected.Count;
    }
}