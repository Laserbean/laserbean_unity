using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Unity.Mathematics;

namespace Laserbean.General
{

    public static class SaveAnything
    {

        public static bool FileExists(string path, string thingName, string extension = "bin")
        {
            PathChecks(ref path, ref extension);
            return File.Exists(path + thingName + "." + extension);
        }

        public static void SaveThing<T>(T thing, string savePath, string thingName, string extension = "bin") where T : class
        {
            PathChecks(ref savePath, ref extension);
            EnsureDirectoryExists(savePath);

            ISaveCallback myObj = thing as ISaveCallback;
            myObj?.OnBeforeSave();


            BinaryFormatter formatter = new();
            FileStream stream = new(savePath + thingName + "." + extension, FileMode.Create);

            formatter.Serialize(stream, thing);
            stream.Close();
        }


        public static T LoadThing<T>(string loadPath, string thingName, string extension = "bin") where T : class
        {
            PathChecks(ref loadPath, ref extension);

            loadPath = loadPath + thingName + "." + extension;
            // Check if a save exists for the name we were passed.
            if (File.Exists(loadPath))
            {

                BinaryFormatter formatter = new();
                FileStream stream = new(loadPath, FileMode.Open);

                T thing = formatter.Deserialize(stream) as T;
                stream.Close();

                ISaveCallback myObj = thing as ISaveCallback;
                myObj?.OnAfterLoad();

                return thing;
            }

            return null;

        }

        static void PathChecks(ref string path, ref string extension)
        {
            if (!path.EndsWith("/"))
                path += "/";
            if (extension.StartsWith("."))
                extension = extension[1..];
        }

        static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static void SaveJson<T>(T thing, string savePath, string thingName, string extension = "json", bool prettyprint = false)
        {
            PathChecks(ref savePath, ref extension);
            EnsureDirectoryExists(savePath);

            ISaveCallback myObj = thing as ISaveCallback;
            myObj?.OnBeforeSave();

            string json = JsonUtility.ToJson(thing, prettyPrint: prettyprint);
            File.WriteAllText(savePath + thingName + "." + extension, json);
        }

        public static void SaveJsonPretty<T>(T thing, string savePath, string thingName, string extension = "json")
        {
            SaveJson(thing, savePath, thingName, extension, true);
        }


        public static T LoadJson<T>(string loadPath, string thingName, string extension = "json") where T : class
        {
            PathChecks(ref loadPath, ref extension);

            loadPath = loadPath + thingName + "." + extension;

            if (File.Exists(loadPath))
            {
                string savedJson = File.ReadAllText(loadPath);
                T jsonthing = JsonUtility.FromJson<T>(savedJson);

                ISaveCallback myObj = jsonthing as ISaveCallback;
                myObj?.OnAfterLoad();
                return jsonthing;
            }
            return null;
        }

        public static string ToJson<T>(this T thing)
        {
            return JsonUtility.ToJson(thing);
        }

        public static T FromJson<T>(this string thing)
        {
            return JsonUtility.FromJson<T>(thing);
        }



        public static string GetUniqueFileName(string path, string filename, string extension)
        {
            PathChecks(ref path, ref extension);

            string newfilename = filename + "." + extension;

            string fullFilePath = Path.Combine(path, newfilename);

            int count = 1;
            while (File.Exists(fullFilePath))
            {
                newfilename = filename + count + "." + extension;
                count++;
            }
            return newfilename;
        }

        public static string GetUniqueFileNameFullPath(string path, string filename, string extension)
        {
            PathChecks(ref path, ref extension);

            string fullFilePath = Path.Combine(path, filename + "." + extension);
            int count = 1;

            while (File.Exists(fullFilePath))
            {
                fullFilePath = Path.Combine(path, filename + count + "." + extension);
                count++;
            }

            return fullFilePath;
        }



    }



    public interface ISaveCallback
    {
        public void OnBeforeSave();

        public void OnAfterLoad();
    }

    [System.Serializable]
    public class StringDict
    {

        [SerializeField]
        public List<string> Keys = new List<string>();
        [SerializeField]
        public List<string> Values = new List<string>();

        public string this[string index]
        {
            get => Values[Keys.IndexOf(index)];
            set => Add(index, value);
        }

        public void ForceAdd(string key, string value)
        {
            if (key == null) throw new System.ArgumentNullException("Key cannot be null");
            if (ContainsKey(key))
            {
                Values[Keys.IndexOf(key)] = value;
                return;
            }

            Keys.Add(key);
            Values.Add(value);
        }

        public void Add(string key, string value)
        {
            if (key == null) throw new System.ArgumentNullException("Key cannot be null");
            if (ContainsKey(key)) throw new System.ArgumentException("Key '{}' already exists.".format(key));

            Keys.Add(key);
            Values.Add(value);

        }

        public bool TryAdd(string key, string value)
        {
            if (key == null || ContainsKey(key)) return false;

            Keys.Add(key);
            Values.Add(value);
            return true;
        }

        // Values[Keys.IndexOf(key)] = value; 


        public void Remove(string key)
        {
            if (!ContainsKey(key)) return;

            int ind = Keys.IndexOf(key);
            Keys.RemoveAt(ind);
            Values.RemoveAt(ind);
        }

        public void Clear()
        {
            Keys.Clear();
            Values.Clear();
        }

        public bool ContainsKey(string key)
        {
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