﻿using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.Hex
{
    public interface IGameGrid
    {
        Vector3 BottomLeftCorner { get; }
        float CellSize { get; }
        int Height { get; }
        int Width { get; }

        Vector3 GetPosition(int x, int y);
    }
}