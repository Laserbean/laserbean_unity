

using System.Diagnostics;

public static class OpenExternalApplication
{

    public static void TryOpen(string externalAppPath)
    {
        try {
            Process.Start(externalAppPath);
        }
        catch (System.Exception e) {
            UnityEngine.Debug.LogError("Ugh, there was an error opening the external app. " + e.Message);
        }
    }

    public static void TryOpen(string externalAppPath, string filepath)
    {
        try {
            Process.Start(externalAppPath, filepath);
        }
        catch (System.Exception e) {
            UnityEngine.Debug.LogError("Ugh, there was an error opening file with the external app. " + e.Message);
        }
    }
}