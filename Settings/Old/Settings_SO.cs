using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General.OldSettings
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Laserbean/OldSettings/Settings", order = 0)]
    public class Settings_SO : ScriptableObject
    {

        public List<SettingsSlot> list = new List<SettingsSlot>();

        // public List<SettingsSlot> GetList() { 
        //     return list;

        // }

    }

    [System.Serializable]
    public class SettingsSlot
    {
        public string name;
        public string displayname;
        [TextArea(3, 5)]
        public string description;
        public string default_value;
        public TypeType type;
        public bool active = true;
    }

}