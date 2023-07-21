using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// public class PolygonDrawer : MonoBehaviour
// {
//     // public LevelGenData lgdata; 
//     // public List<RegionData> regions;

//     public List<List<Vector2>> polygons;

//     // private void OnDrawGizmos()
//     // {
//     //     regions = lgdata.regions; 
//     //     if (regions != null) {

//     //         Gizmos.color = Color.green;
//     //         foreach (var region in regions)
//     //         {
//     //             List<Vector2> polygon = region.points; 
//     //             GizmoHelper.DrawPolygon(polygon, Color.green); 
//     //         }

//     //     }
//     // }

//     // private void OnDrawGizmos()
//     // {
//     //     regions = lgdata.regions; 
//     //     if (polygons != null) {

//     //         Gizmos.color = Color.green;
//     //         foreach (var polygon in polygons)
//     //         {
//     //             for (int i = 0; i < polygon.Count; i++)
//     //             {
//     //                 var start = polygon[i];
//     //                 var end = polygon[(i + 1) % polygon.Count];
//     //                 Gizmos.DrawLine(start, end);
//     //             }
//     //         }

//     //     }
//     // }
// } 


public static class GizmoHelper {

    public static void Draw(this PolygonCollider2D polygonCollider, Color color, Transform parent = null)
    {
        Vector2 origin = polygonCollider.offset; 
        if (parent != null) {
            // origin = parent.position; 
            Gizmos.matrix = parent.localToWorldMatrix;
        }
        Gizmos.color = color;
        for (int i = 0; i < polygonCollider.pathCount; i++)
        {
            Vector2[] points = polygonCollider.GetPath(i);
            for (int j = 0; j < points.Length - 1; j++)
            {
                Gizmos.DrawLine(points[j] + origin, points[j + 1] + origin);
            }
            Gizmos.DrawLine(points[points.Length - 1] + origin, points[0] + origin);
        }
    }

    public static void Draw(this BoxCollider2D boxCollider, Color color, Transform parent = null)
    {
        Vector2 origin = Vector2.zero; 
        if (parent != null) {
            // origin = parent.position;  
            Gizmos.matrix = parent.localToWorldMatrix;
        }

        
        Gizmos.color = color;
        Vector2 size = boxCollider.size;
        Vector2 position = boxCollider.offset + origin;
        Vector2 min = position - size / 2f;
        Vector2 max = position + size / 2f;

        Gizmos.DrawLine(min, new Vector2(min.x, max.y));
        Gizmos.DrawLine(new Vector2(min.x, max.y), max);
        Gizmos.DrawLine(max, new Vector2(max.x, min.y));
        Gizmos.DrawLine(new Vector2(max.x, min.y), min);

        // Gizmos.color = color;
        // Gizmos.matrix = boxCollider.transform.localToWorldMatrix;
        // Gizmos.DrawWireCube(boxCollider.offset, boxCollider.size);
    }

    public static void Draw(this CircleCollider2D circleCollider, Color color, Transform parent = null)
    {
        Vector2 origin = Vector2.zero; 
        if (parent != null) {
            // origin = parent.position;  
            Gizmos.matrix = parent.localToWorldMatrix;
        }

        Gizmos.color = color;
        Vector2 position = circleCollider.offset + origin;
        float radius = circleCollider.radius;
        int segments = 36;
        float angleIncrement = 2f * Mathf.PI / segments;

        Vector2 startPoint = new Vector2(radius, 0f) + position;
        Vector2 prevPoint = startPoint;

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleIncrement;
            float x = Mathf.Cos(angle) * radius + position.x;
            float y = Mathf.Sin(angle) * radius + position.y;
            Vector2 point = new Vector2(x, y);

            Gizmos.DrawLine(prevPoint, point);

            prevPoint = point;
        }

        Gizmos.DrawLine(prevPoint, startPoint);
    }


    public static void Draw(this Collider2D collider, Color color, Transform parent = null) {
        if (collider is BoxCollider2D col){
            col.Draw(color, parent);
        }

        if (collider is CircleCollider2D col1){
            col1.Draw(color, parent);
        }
        if (collider is PolygonCollider2D col2){
            col2.Draw(color, parent);
        }
    }

    public static void DrawPolygon(List<Vector2> points, Color color) {
        Gizmos.color = color;
        for (int i = 0; i < points.Count; i++)
        {
            var start = points[i];
            var end = points[(i + 1) % points.Count];
            Gizmos.DrawLine(start, end);
        }

    }

    public static void DrawPolygons(List<List<Vector2>> polygons, Color color) {
        foreach (var polygon in polygons) {
            DrawPolygon(polygon, color); 
        }
    }


    // public static void DrawRect(Rect rect, Color color) {
    //     // var rect = new Rect(0f, 0f, 100f, 100f);
    //     Debug.DrawLine(new Vector3(rect.x, rect.y), new Vector3(rect.x + rect.width, rect.y ),color);
    //     Debug.DrawLine(new Vector3(rect.x, rect.y), new Vector3(rect.x , rect.y + rect.height), color);
    //     Debug.DrawLine(new Vector3(rect.x + rect.width, rect.y + rect.height), new Vector3(rect.x + rect.width, rect.y), color);
    //     Debug.DrawLine(new Vector3(rect.x + rect.width, rect.y + rect.height), new Vector3(rect.x, rect.y + rect.height), color);
    // }

    public static void DrawRect(Rect rect, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y));
        Gizmos.DrawLine(new Vector2(rect.x + rect.width, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height));
        Gizmos.DrawLine(new Vector2(rect.x + rect.width, rect.y + rect.height), new Vector2(rect.x, rect.y + rect.height));
        Gizmos.DrawLine(new Vector2(rect.x, rect.y + rect.height), new Vector2(rect.x, rect.y));
    }

}

