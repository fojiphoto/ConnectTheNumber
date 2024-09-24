using UnityEngine;

namespace Ilumisoft.Hex
{
    public class SelectionLineRenderer : MonoBehaviour
    {
        [SerializeField]
        LineRenderer lineRenderer = null;

        public void Clear()
        {
            lineRenderer.positionCount = 0;
        }

        public void UpdateLine(ISelection selection)
        {
            lineRenderer.positionCount = 0;

            for (int i = 0; i < selection.Count; i++)
            {
                if(i>0)
                {
                    var previous = selection.Get(i - 1);
                    var current = selection.Get(i);

                    AddLineSegment(previous.transform.position, current.transform.position);
                }
            }
        }

        void AddLineSegment(Vector3 start, Vector3 end)
        {
            //We add line segments by adding multiple points of the line segment to the line renderer.
            //Otherwise (since Line Renderer Component generates a mesh at runtime) the line segments
            //might not be of equal width
            int steps = 5;
            float stepSize = 1.0f / steps;

            for (int i = 0; i <= steps; i++)
            {
                Vector3 center = Vector3.Lerp(start, end, i*stepSize);
                AddPoint(center);
            }
        }

        void AddPoint(Vector3 position)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector2(position.x, position.y));
        }

        private void Reset()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
    }
}