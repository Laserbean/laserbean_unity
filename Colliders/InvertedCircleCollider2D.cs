using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Laserbean.Colliders
{
    [RequireComponent(typeof(EdgeCollider2D))]
    public class InvertedCircleCollider2D : MonoBehaviour
    {
        public float Radius;

        [Range(2, 100)]
        public int NumEdges;

        private void Start()
        {
            Generate();
        }

        private void OnValidate()
        {
            Generate();
        }

        private void Generate()
        {
            EdgeCollider2D edgeCollider2D = GetComponent<EdgeCollider2D>();
            Vector2[] points = new Vector2[NumEdges + 1];

            for (int i = 0; i < NumEdges; i++)
            {
                float angle = 2 * Mathf.PI * i / NumEdges;
                float x = Radius * Mathf.Cos(angle);
                float y = Radius * Mathf.Sin(angle);

                points[i] = new Vector2(x, y);
            }
            points[NumEdges] = points[0];

            edgeCollider2D.points = points;
        }
    }

}