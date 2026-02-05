using UnityEngine;

public class DebugLogger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static void DebugLog(string text)
    {
        Debug.Log(text);
    }

    public static void DebugLogError(string text)
    {
        Debug.LogError(text);
    }

    public static void DebugLogWarning(string text)
    {
        Debug.LogWarning(text);
    }

}
