using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public static class FileImportHandlerMenu
{
    [MenuItem("Tools/Auto file move")]
    private static void AutoRefreshToggle()
    {
        var status = EditorPrefs.GetInt("kAutoFileMove");

        EditorPrefs.SetInt("kAutoFileMove", status == 1 ? 0 : 1);
    }

    [MenuItem("Tools/Auto file move", true)]
    private static bool AutoRefreshToggleValidation()
    {
        var status = EditorPrefs.GetInt("kAutoFileMove");
        Menu.SetChecked("Tools/Auto file move", status == 1);
        return true;
    }

}