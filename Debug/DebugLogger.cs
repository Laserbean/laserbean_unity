using UnityEngine;

public class DebugLogger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void DebugLog(string text)
    {
        Debug.Log(text); 
    }

    public void DebugLogError(string text)
    {
        Debug.LogError(text); 

    }

    public void DebugLogWarning(string text)
    {
        Debug.LogWarning(text); 
    }

}
