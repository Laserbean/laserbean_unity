using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;




public static class SaveAnything 
{
    public static void SaveThing <T> (T thing, string thingName, string savePath, string extension) {
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(savePath + thingName + "." + extension, FileMode.Create);

        formatter.Serialize(stream, thing);
        stream.Close();
    }


    public static T LoadThing<T> (string thingName, string loadPath, string extension) where T : class{

        loadPath = loadPath + thingName + "." + extension; 
        // Check if a save exists for the name we were passed.
        if (File.Exists(loadPath)) {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(loadPath, FileMode.Open);

            T thing = formatter.Deserialize(stream) as T;
            stream.Close();

            return thing;
        }

        return null;
        
    }

}