
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

using System.Linq;
using System.IO;



public static class HelpfulFunctions {
    public static List<T> Overlap<T>(this List<T> thislist, List<T> another) {

        // List<string> res = (List<string>) tags.AsQueryable().Intersect(_tags);
        List<T> res = thislist.Intersect(another).ToList<T>();
        return res;     
    }
}


#if UNITY_EDITOR

public static class ScriptableObjectUtility
{
    public static T CreateAsset<T>(string filename = null, string path = null) where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        // string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (string.IsNullOrEmpty(path))
        {
            path = "Assets";
        }
        // else  if (Path.GetExtension(path) != "")
        // {
        //     path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        // }

        if (string.IsNullOrEmpty(filename)) {
            filename = typeof(T).ToString(); 
        }
        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/ " + filename + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        // EditorUtility.FocusProjectWindow();
        // Selection.activeObject = asset;
        return asset; 
    }

    public static string FindAssetPathRelativeToDrawer(this ScriptableObject asset, string assetName)
    {
        string[] guids = AssetDatabase.FindAssets(assetName);

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            if (assetPath.Contains(asset.GetType().Name))
                return assetPath;
        }

        return string.Empty;
    }
}

#endif
