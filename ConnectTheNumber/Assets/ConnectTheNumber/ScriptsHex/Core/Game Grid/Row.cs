using System;
using UnityEngine;

namespace Ilumisoft.Hex
{
    public class Row : MonoBehaviour, IRow
    {
        [SerializeField]
        float cellSize = 1.0f;

        [SerializeField]
        int width = 0;

        public int Width { get => width; set { width = value; } }

        private void OnDrawGizmos()
        {
            for (int x = 0; x < width; x++)
            {
                var cellPos = GetPosition(x);

                Gizmos.DrawWireCube(cellPos, Vector3.one * cellSize);
            }
        }

        private Vector3 GetPosition(int x)
        {
            var centerOfRow = transform.position;

            float widthOfRow = Width * cellSize;

            var mostLeftCellPosition = centerOfRow - Vector3.right * (widthOfRow - cellSize) / 2;

            return mostLeftCellPosition + Vector3.right * x * cellSize;
        }
    }
}