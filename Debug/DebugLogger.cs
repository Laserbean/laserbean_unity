using UnityEngine;

public class DebugLogger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [HideInCallstack]
    public void DebugLog(string text)
    {
        Debug.Log(text);
    }
    [HideInCallstack]
    public void DebugLogError(string text)
    {
        Debug.LogError(text);

    }
    [HideInCallstack]
    public void DebugLogWarning(string text)
    {
        Debug.LogWarning(text);
    }

}
