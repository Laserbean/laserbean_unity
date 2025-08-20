using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

//purpose of this postprocessor is to ensure that listed file extensions
// are not in certain filepaths, when they are they are moved to a 
//specified default path
public class FileImportHandler : AssetPostprocessor
{
    //only evaluate files imported into these paths
    static List<string> pathsToMoveFrom = new()
    {
        "Assets"
    };

    // static Dictionary<string, string> defaultFileLocationByExtension = new()
    // {
    //     {".mp4",   "Assets/StreamingAssets/"},//for IOS, movies need to be in StreamingAssets

    // 	{".anim",   "Assets/Art/Animations/"},
    //     {".mat",    "Assets/Art/Materials/"},
    //     {".fbx",    "Assets/Art/Meshes/"},

    // 	//Images has subfolders for Textures, Maps, Sprites, etc.
    // 	// up to the user to properly sort the images folder
    // 	{".bmp",    "Assets/Art/Images/"},
    //     {".png",    "Assets/Art/Images/"},
    //     {".jpg",    "Assets/Art/Images/"},
    //     {".jpeg",   "Assets/Art/Images/"},
    //     {".psd",    "Assets/Art/Images/"},

    //     {".mixer",    "Assets/Audio/Mixers/"},
    //     //like images, there are sub folders that the user must manage
    // 	{".wav",    "Assets/Audio/Sources/"}, 

    //     //like images, there are sub folders that the user must manage
    // 	{".cs",     "Assets/Scripts/"},
    //     {".shader", "Assets/Shaders/"},
    //     {".cginc",  "Assets/Shaders/"}
    // };


    static Dictionary<string, string> defaultFileLocationByExtension = new()
    {
        {".mp4",   "Assets/_Assets/StreamingAssets/"},//for IOS, movies need to be in StreamingAssets

		{".anim",   "Assets/_Assets/Art/Animations/"},
        {".mat",    "Assets/_Assets/Art/Materials/"},
        {".fbx",    "Assets/_Assets/Art/Meshes/"},

		//Images has subfolders for Textures, Maps, Sprites, etc.
		// up to the user to properly sort the images folder
		{".bmp",    "Assets/_Assets/Art/Images/"},
        {".png",    "Assets/_Assets/Art/Images/"},
        {".jpg",    "Assets/_Assets/Art/Images/"},
        {".jpeg",   "Assets/_Assets/Art/Images/"},
        {".psd",    "Assets/_Assets/Art/Images/"},

        {".mixer",    "Assets/_Assets/Audio/Mixers/"},
        //like images, there are sub folders that the user must manage
		{".wav",    "Assets/_Assets/Audio/Sources/"}, 

        //like images, there are sub folders that the user must manage
		{".cs",     "Assets/_Assets/Scripts/"},
        {".shader", "Assets/_Assets/Shaders/"},
        {".cginc",  "Assets/_Assets/Shaders/"},
        {".physicsMaterial2D",  "Assets/_Assets/PhysicsMaterials2D/"}
    };

    // static readonly List<string> blacklistPath = new()
    // {
    //     {  "Assets/Scripts/"}
    // };




    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        var status = EditorPrefs.GetInt("kAutoFileMove");
        if (status == 0) return;

        foreach (string oldFilePath in importedAssets)
        {
            string directory = Path.GetDirectoryName(oldFilePath);
            if (!pathsToMoveFrom.Contains(directory))
                continue;

            string extension = Path.GetExtension(oldFilePath).ToLower();
            if (!defaultFileLocationByExtension.ContainsKey(extension))
                continue;

            string filename = Path.GetFileName(oldFilePath);
            string newPath = defaultFileLocationByExtension[extension];



            if (!AssetDatabase.IsValidFolder(newPath))
            {
                System.IO.Directory.CreateDirectory(newPath);
                Debug.Log("Folder created: " + newPath);
                AssetDatabase.Refresh();
            }

            AssetDatabase.MoveAsset(oldFilePath, newPath + filename);




            Debug.Log(string.Format("Moving asset ({0}) to path: {1}", filename, newPath));
        }
    }
}

