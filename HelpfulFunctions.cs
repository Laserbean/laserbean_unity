
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

using System.Linq;
using System.IO;


namespace Laserbean.General {

public static class HelpfulFunctions {
    public static List<T> Overlap<T>(this List<T> thislist, List<T> another) {

        // List<string> res = (List<string>) tags.AsQueryable().Intersect(_tags);
        List<T> res = thislist.Intersect(another).ToList<T>();
        return res;     
    }


    public static void Resize<T>(ref List<T> array, int new_length, T thing = default) {
        List<T> newArray = new (new_length);

        for (int i = 0; i < Mathf.Min(array.Count, new_length); i++) {
            newArray.Add(thing); 
            newArray[i] = array[i];
            // if (array[i] != null)
            //     newArray.Add(array[i]);
        }
        for (int i = Mathf.Min(array.Count, new_length); i < new_length; i++) {
            // newArray[i] = new T();
            newArray.Add(thing); 
        }

        // if (new_length > array.Count) {
        //     for (int i = Mathf.Min(array.Count, new_length); i < Mathf.Max(array.Count, new_length); i++) {
        //         // newArray[i] = new T();
        //     }
        // } else {

        // }

        if (newArray.Count != new_length) {
            throw new System.Exception("fish"); 
        }
        array = newArray; 
    }



    // public static void Resize<T>(ref T[] array, int new_length) {
    //     T[] newArray = new T[new_length];


    //     for (int i = 0; i < Mathf.Min(array.Length, new_length); i++) {
    //         newArray[i] = array[i];
    //     }


    //     if (new_length > array.Length) {
    //         for (int i = Mathf.Min(array.Length, new_length); i < Mathf.Max(array.Length, new_length); i++) {
    //             newArray[i] = new T();
    //         }
    //     } else {

    //     }
    //     array = newArray; 
    // }

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

}
