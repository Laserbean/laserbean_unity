using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

public class SettingsManager : Singleton<SettingsManager>
{
    [SerializeField] bool debug = true;

    [SerializeField]
    public Settings settings;

    public Settings_SO settings_so;


    [SerializeField] GameObject content;

    [SerializeField] GameObject settings_prefab;
    [SerializeField] GameObject header_prefab;


    public bool Ready = false;


    public float yspace = 10f;
    public Vector3 offset;

    Dictionary<string, SettingsSlot> slotsdict = new();

    private void Start()
    {
        // Settings loaded = LoadSettings(); 
        Loadtest();
        for (int i = 0; i < settings_so.list.Count; i++) {

            if (settings_so.list[i].name == "header") {
                GameObject go2 = Instantiate(header_prefab, content.transform.position + new Vector3(0, -i * yspace, 0) + offset, content.transform.rotation);
                go2.transform.SetParent(content.transform);
                go2.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = settings_so.list[i].displayname;
                continue;
            }
            slotsdict.Add(settings_so.list[i].name, settings_so.list[i]);

            if (!settings_so.list[i].active) {
                continue;
            }


            GameObject go = Instantiate(settings_prefab, content.transform.position + new Vector3(0, -i * yspace, 0) + offset, content.transform.rotation);
            go.transform.SetParent(content.transform);
            go.GetComponent<SettingItemController>().Init(settings_so.list[i].name, settings_so.list[i].type, settings_so.list[i].displayname, settings_so.list[i].description);
        }
        Ready = true;
        Actions.OnSettingsChange();
    }


    public void UpdateSetting(string name, int val)
    {
        settings.Set(name, val);
        Savetest();
    }
    public void UpdateSetting(string name, bool val)
    {
        settings.Set(name, val);
        Savetest();
    }
    public void UpdateSetting(string name, float val)
    {
        settings.Set(name, val);
        Savetest();
    }
    public void UpdateSetting(string name, string val)
    {
        settings.Set(name, val);
        Savetest();
    }

    public int GetInt(string name)
    {
        if (!settings.ContainsKey(name)) {
            settings.Add(name, int.Parse(slotsdict[name].default_value));
        }
        return settings.GetInt(name);

    }
    public float GetFloat(string name)
    {
        if (!settings.ContainsKey(name)) {
            settings.Add(name, float.Parse(slotsdict[name].default_value));
        }
        return settings.GetFloat(name);
    }
    public string GetString(string name)
    {
        if (!settings.ContainsKey(name)) {
            settings.Add(name, slotsdict[name].default_value);
        }
        return settings.GetString(name);
    }
    public bool GetBool(string name)
    {
        if (!settings.ContainsKey(name)) {
            settings.Add(name, bool.Parse(slotsdict[name].default_value));
        }
        return settings.GetBool(name);
    }

    [ContextMenu("savetest")]
    public void Savetest()
    {
        SaveSettings(settings);
        Debug.Log("saved");
        Actions.OnSettingsChange();

    }

    [ContextMenu("loadtest")]
    public void Loadtest()
    {
        settings = LoadSettings();
        Debug.Log("loaded");
    }

    public static void SaveSettings(Settings ddd)
    {

        // // Set our save location and make sure we have a saves folder ready to go.
        string savePath = GameManager.Instance.AppPath + "/";

        // // If not, create it.
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);



        string output = JsonUtility.ToJson(ddd);
        File.WriteAllText(savePath + "settings.json", output);
    }

    public static Settings LoadSettings()
    {
        // Get the path to our world saves.
        string loadPath = GameManager.Instance.AppPath + "/";

        // Check if a save exists for the name we were passed.
        if (File.Exists(loadPath + "settings.json")) {
            string input = File.ReadAllText(loadPath + "settings.json");
            Settings world = JsonUtility.FromJson<Settings>(input);

            return world;

        }
        else {
            return new Settings();
        }
    }
}



public enum TypeType
{
    _int, _float, _bool, _string
}


[System.Serializable]
public class Settings
{

    [SerializeField] public List<string> keylist = new();
    // [SerializeField] public List<TypeType> typelist = new List<TypeType>(); 

    [SerializeField] public List<string> stringlist = new();


    public void Add(string name, int val)
    {
        keylist.Add(name);
        // typelist.Add(TypeType._int); 
        stringlist.Add(val.ToString());
    }

    public void Add(string name, string val)
    {
        keylist.Add(name);
        // typelist.Add(TypeType._string); 
        stringlist.Add(val.ToString());
    }

    public void Add(string name, float val)
    {
        keylist.Add(name);
        // typelist.Add(TypeType._float); 
        stringlist.Add(val.ToString());
    }

    public void Add(string name, bool val)
    {
        keylist.Add(name);
        // typelist.Add(TypeType._bool); 
        stringlist.Add(val.ToString());
    }

    public int GetInt(string name)
    {
        int i = keylist.IndexOf(name);
        return int.Parse(stringlist[i]);
    }

    public bool GetBool(string name)
    {
        int i = keylist.IndexOf(name);
        return bool.Parse(stringlist[i]);
    }

    public float GetFloat(string name)
    {
        int i = keylist.IndexOf(name);
        return float.Parse(stringlist[i]);
    }

    public string GetString(string name)
    {
        int i = keylist.IndexOf(name);
        return stringlist[i];
    }

    public void Set(string name, int x)
    {
        int i = keylist.IndexOf(name);
        stringlist[i] = x.ToString();
    }

    public void Set(string name, float x)
    {
        int i = keylist.IndexOf(name);
        stringlist[i] = x.ToString();
    }

    public void Set(string name, bool x)
    {
        int i = keylist.IndexOf(name);
        stringlist[i] = x.ToString();
    }

    public void Set(string name, string x)
    {
        int i = keylist.IndexOf(name);
        stringlist[i] = x;
    }

    public bool ContainsKey(string name)
    {
        return keylist.Contains(name);
    }






}
