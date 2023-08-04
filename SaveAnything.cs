using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;


namespace Laserbean.General
{

public static class SaveAnything 
{

    public static bool FileExists(string loadPath, string thingName, string extension = "bin") {
        if (!loadPath.EndsWith("/")) loadPath = loadPath + "/"; 
        if (extension.StartsWith(".")) extension = extension.Substring(1, extension.Length-1); 

        return File.Exists(loadPath); 
    }

    public static void SaveThing <T> (T thing, string savePath, string thingName, string extension = "bin") where T : class
    {

        if (!savePath.EndsWith("/")) savePath = savePath + "/"; 
        if (extension.StartsWith(".")) extension = extension.Substring(1, extension.Length-1); 

        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        
        ISaveCallback myObj = thing as ISaveCallback;
        myObj?.OnBeforeSave(); 


        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(savePath + thingName + "." + extension, FileMode.Create);

        formatter.Serialize(stream, thing);
        stream.Close();
    }


    public static T LoadThing<T> (string loadPath, string thingName, string extension = "bin") where T : class 
    {
        if (!loadPath.EndsWith("/")) loadPath = loadPath + "/"; 
        if (extension.StartsWith(".")) extension = extension.Substring(1, extension.Length-1); 


        loadPath = loadPath + thingName + "." + extension; 
        // Check if a save exists for the name we were passed.
        if (File.Exists(loadPath)) {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(loadPath, FileMode.Open);

            T thing = formatter.Deserialize(stream) as T;
            stream.Close();

            ISaveCallback myObj = thing as ISaveCallback;
            myObj?.OnAfterLoad(); 

            return thing;
        }

        return null;
        
    }

    public static void SaveJson<T>(T thing, string savePath, string thingName, string extension = "json") {
        if (!savePath.EndsWith("/")) savePath = savePath + "/"; 
        if (extension.StartsWith(".")) extension = extension.Substring(1, extension.Length-1); 

        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
            
        string json = JsonUtility.ToJson(thing);

        //Write the JSON string to a file on disk.
        File.WriteAllText(savePath + thingName + "." + extension , json);
    }
 

    public static T LoadJson<T> (string loadPath, string thingName, string extension = "json") where T : class{    

        if (!loadPath.EndsWith("/")) loadPath = loadPath + "/"; 
        if (extension.StartsWith(".")) extension = extension.Substring(1, extension.Length-1); 

        loadPath = loadPath + thingName + "." + extension; 

        if (File.Exists(loadPath)) {

            //Get the JSON string from the file on disk.
            string savedJson = File.ReadAllText(loadPath);

            //Convert the JSON string back to a ConfigData object.
            T jsonthing = JsonUtility.FromJson<T>(savedJson);
            return jsonthing;
        }
        return null;
    }


}



public interface ISaveCallback {
    public void OnBeforeSave();

    public void OnAfterLoad(); 
}

[System.Serializable]
public class StringDict {

    [SerializeField]
    public List<string> Keys = new List<string>();
    [SerializeField]
    public List<string> Values = new List<string>();

    public string this[string index]
    {
        get => Values[Keys.IndexOf(index)];
        set => Add(index, value);
    }

    public void ForceAdd(string key, string value) {
        if (key == null) throw new System.ArgumentNullException("Key cannot be null");
        if (ContainsKey(key)) {
            Values[Keys.IndexOf(key)] = value; 
            return;
        }

        Keys.Add(key);
        Values.Add(value); 
    }

    public void Add(string key, string value) {
        if (key == null) throw new System.ArgumentNullException("Key cannot be null");
        if (ContainsKey(key)) throw new System.ArgumentException("Key '{}' already exists.".format(key));

        Keys.Add(key);
        Values.Add(value); 
        
    }

    public bool TryAdd(string key, string value) {
        if (key == null || ContainsKey(key)) return false; 

        Keys.Add(key);
        Values.Add(value); 
        return true; 
    }

            // Values[Keys.IndexOf(key)] = value; 


    public void Remove(string key) {
        if (!ContainsKey(key)) return; 
        
        int ind = Keys.IndexOf(key);
        Keys.RemoveAt(ind);
        Values.RemoveAt(ind);
    }

    public void Clear() {
        Keys.Clear();
        Values.Clear(); 
    }

    public bool ContainsKey(string key) {
        return Keys.Contains(key); 
    }


}


// [System.Serializable]
// public class StringDict2 {

//     [System.Serializable] 
//     public class KeyPair {
//         public string key;
//         public string value; 

//         public KeyPair(string k, string v) {
//             key = k; value = v; 
//         }
//     }

//     [SerializeField]
//     public List<KeyPair> data = new List<KeyPair>();


//     public string this[string index]
//     {
//         get => data[GetIndex(index)].value;
//         set => Add(index, value);
//     }

//     int GetIndex(string key) {
//         int i = 0;
//         foreach(var kvp in data) {
//             if (kvp.key == key) {
//                 return i; 
//             }
//             i += 1; 
//         }
//         return -1; 
//     }

//     // public void ForceAdd(string key, string value) {
//     //     if (key == null) throw new System.ArgumentNullException("Key cannot be null");
//     //     if (ContainsKey(key)) {
//     //         Values[Keys.IndexOf(key)] = value; 
//     //         return;
//     //     }

//     //     Keys.Add(key);
//     //     Values.Add(value); 
//     // }

//     public void Add(string key, string value) {
//         if (key == null) throw new System.ArgumentNullException("Key cannot be null");
//         if (ContainsKey(key)) throw new System.ArgumentException("Key '{}' already exists.".format(key));

//         data.Add(new KeyPair(key, value)); 
        
//     }

//     // public bool TryAdd(string key, string value) {
//     //     if (key == null || ContainsKey(key)) return false; 

//     //     Keys.Add(key);
//     //     Values.Add(value); 
//     //     return true; 
//     // }

//             // Values[Keys.IndexOf(key)] = value; 


//     // public void Remove(string key) {
//     //     if (!ContainsKey(key)) return; 
        
//     //     int ind = Keys.IndexOf(key);
//     //     Keys.RemoveAt(ind);
//     //     Values.RemoveAt(ind);
//     // }

//     // public void Clear() {
//     //     Keys.Clear();
//     //     Values.Clear(); 
//     // }

//     public bool ContainsKey(string key) {
//         foreach(var kvp in data) {
//             if (kvp.key == key) {
//                 return true;  
//             }
//         }
//         return false; 
//     }


// }

}