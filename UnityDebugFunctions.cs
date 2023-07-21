using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityDebugFunctions
{
    public static void DrawRect(Rect rect, Color color) {
        // var rect = new Rect(0f, 0f, 100f, 100f);
        Debug.DrawLine(new Vector3(rect.x, rect.y), new Vector3(rect.x + rect.width, rect.y ),color);
        Debug.DrawLine(new Vector3(rect.x, rect.y), new Vector3(rect.x , rect.y + rect.height), color);
        Debug.DrawLine(new Vector3(rect.x + rect.width, rect.y + rect.height), new Vector3(rect.x + rect.width, rect.y), color);
        Debug.DrawLine(new Vector3(rect.x + rect.width, rect.y + rect.height), new Vector3(rect.x, rect.y + rect.height), color);
    }

}



