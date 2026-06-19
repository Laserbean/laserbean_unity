#define USINGVISUALCONSOLE

using UnityEngine;
using System;
using Laserbean.General;

// using chrisGameDev.VisualConsole;


#if USINGVISUALCONSOLE
using chrisGameDev.VisualConsole;
#endif


public class LaserbeanLogger : MonoBehaviour
{
    [SerializeField] bool EnableLogs = true;
    private void OnInspectorUpdate()
    {
        Lglog.EnableLogs = EnableLogs;
    }
}

#if USINGVISUALCONSOLE
#else
#endif

public static class Lglog
{
    public static bool EnableLogs = true;

    public static void Log(string message)
    {
#if USINGVISUALCONSOLE
        Debug.Log(message);
#else
        Debug.Log(message);
#endif
    }

    public static void Log(string message, string id)
    {
#if USINGVISUALCONSOLE
        VisualDebug.NestingLog(id, message);
#else
        Debug.Log(message);
#endif
    }

    public static void CloseNest(string id)
    {
#if USINGVISUALCONSOLE
        VisualDebug.NestingLogEnd(id); 
#else
        // // Debug.Log(message);
#endif
    }


    public static void LogError(string message)
    {
        Debug.LogError(message);
    }

    public static void LogWarning(string message)
    {
        Debug.LogWarning(message);
    }
}

