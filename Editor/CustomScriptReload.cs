#define CUSTOM_SCRIPT_RELOAD

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//https://forum.unity.com/threads/can-i-stop-auto-compile-after-edit-create-or-remove-a-script.1268315/

#if CUSTOM_SCRIPT_RELOAD

public static class CustomScriptReload
{

    [MenuItem("Sklapps/Editor/Auto Refresh")]
    private static void AutoRefreshToggle()
    {
        var status = EditorPrefs.GetInt("kAutoRefresh");

        EditorPrefs.SetInt("kAutoRefresh", status == 1 ? 0 : 1);
    }

    [MenuItem("Sklapps/Editor/Auto Refresh", true)]
    private static bool AutoRefreshToggleValidation()
    {
        var status = EditorPrefs.GetInt("kAutoRefresh");

        Menu.SetChecked("Sklapps/Editor/Auto Refresh", status == 1);

        return true;
    }

    [MenuItem("Sklapps/Editor/Refresh %r")]
    private static void Refresh()
    {
        Debug.Log("Request script reload.");

        EditorApplication.UnlockReloadAssemblies();

        AssetDatabase.Refresh();

        EditorUtility.RequestScriptReload();
    }

    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        Debug.Log("Script realoded!");

        AssetDatabase.SaveAssets();

        EditorApplication.LockReloadAssemblies();
    }
}

#endif