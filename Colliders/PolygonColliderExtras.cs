using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PolygonColliderExtras
{

    /// <summary>
    /// Sets the points of a polygon collider 2d into the desired sector shape. 
    /// </summary>
    /// <param name="angle"> Size/angle of sector.</param>
    /// <param name="startAngle">Offset angle</param>
    /// <param name="outerRadius">Outer radius</param>
    /// <param name="innerRadius">inner radius</param>
    /// <param name="numPoints">Number of points/lines that would make the outer and inner parameter.</param>    
    public static void GenerateSectorCollider(this PolygonCollider2D collider, float angle, float startAngle, float outerRadius, float innerRadius, int numPoints)
    {

        Vector2 offset = collider.offset;// + (Vector2) collider.gameObject.transform.position; 

        // Calculate the angle increment between each point
        float angleIncrement = angle / numPoints;

        // Create arrays to hold the outer and inner points of the sector
        Vector2[] outerPoints = new Vector2[numPoints + 1];
        Vector2[] innerPoints = new Vector2[numPoints + 1];

        // Generate the points for the sector shape
        for (int i = 0; i <= numPoints; i++)
        {
            float currentAngle = startAngle + angleIncrement * i;
            float cosAngle = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float sinAngle = Mathf.Sin(currentAngle * Mathf.Deg2Rad);

            // Calculate the position of the outer and inner points
            Vector2 outerPoint = new Vector2(outerRadius * cosAngle, outerRadius * sinAngle);
            Vector2 innerPoint = new Vector2(innerRadius * cosAngle, innerRadius * sinAngle);

            // Add the points to the arrays
            outerPoints[i] = outerPoint + offset;
            innerPoints[i] = innerPoint + offset;
        }

        // Concatenate the outer and inner point arrays to form the collider path
        Vector2[] colliderPath = new Vector2[numPoints * 2 + 2];
        outerPoints.CopyTo(colliderPath, 0);
        System.Array.Reverse(innerPoints);
        innerPoints.CopyTo(colliderPath, numPoints + 1);

        // Set the collider path to the polygon collider
        collider.SetPath(0, colliderPath);

        // return collider;
    }




    /// <summary>
    /// Sets the points of a n EdgeCollider2D collider into the desired inverted circle. 
    /// </summary>
    /// <param name="radius"> Size/angle of sector.</param>
    /// <param name="numEdges">Offset angle</param>
    public static void GenerateInvertedCircleCollider(this EdgeCollider2D edgeCollider2D, float radius, int numEdges)
    {
        // EdgeCollider2D edgeCollider2D = GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[numEdges + 1];

        for (int i = 0; i < numEdges; i++)
        {
            float angle = 2 * Mathf.PI * i / numEdges;
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            points[i] = new Vector2(x, y);
        }
        points[numEdges] = points[0];

        edgeCollider2D.points = points;
    }



}
